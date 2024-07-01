using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    BoxCollider2D box;
    Rigidbody2D rb;
    Animator animator;
    SpellCooldown sc;

    public LayerMask Ground;
    public float runSpeed = 7f, jumpSpeed = 12f;
    [HideInInspector] public bool faceingRight = true;
    [SerializeField] private Joystick joystick;
    float move, horizontalInput, joystickInput, verticalMove;
    string[] listStates = { "Player atk2", "Player atk3", "Player atk4" };

    public GameObject theThan;
    [SerializeField] private AudioSource dash_audio;
    bool canDash = true;
    bool isDashing;
    public float dashingPower = 300f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.8f;
    TrailRenderer tr;
    bool clickDash;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sc = GetComponent<SpellCooldown>();
        tr = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");//di chuyển ngang từ bàn phím
        joystickInput = joystick.Horizontal;//d/c ngang từ joystick
        move = horizontalInput + joystickInput;//kết hợp cả 2 đầu vào
        verticalMove = joystick.Vertical;//d/c nhảy
        animator.SetBool("Run", move != 0);
        flip();

        //nhảy
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) || verticalMove > 0.2f)
        {
            if (IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
        }
        if (rb.velocity.y > 0)
        {
            animator.SetBool("Jump", true);
        }
        if (rb.velocity.y < 0)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", true);
        }
        if (IsGrounded())
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", false);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(move * runSpeed, rb.velocity.y);

        //dash
        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.Keypad3) || clickDash)
        {
            clickDash = false;//
            if (canDash && !IsInSpecificStates(listStates))
            {
                StartCoroutine(Dash());

                sc.UseSpellDash();//Spell Cooldown
            }
        }
        if (isDashing)
        {//ngăn player move,jump,.. khi dash
            return;
        }

        if (IsInSpecificStates("Player atk1") || IsInSpecificStates("Player Hurt"))
        {//khi atk1 or bị thương thì ko d/c dc, chỉ nhảy dc
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (IsInSpecificStates(listStates))
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void ClickDash()
    {//click phím có chức năng dash trên giao diện game
        clickDash = true;
    }

    void flip()
    {
        if (!IsInSpecificStates(listStates))
        {
            if (move > 0 && !faceingRight || move < 0 && faceingRight)
            {
                /*faceingRight = !faceingRight;
                transform.Rotate(0, 180, 0);*/
                Vector3 localScale = transform.localScale;
                faceingRight = !faceingRight;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D ray = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.1f, Ground);
        return ray.collider != null;
    }

    private IEnumerator Dash()
    {
        dash_audio.Play();

        //ở Rigibody 2D > Collision Detection > Continuos
        //Continuos: để xử lý va chạm chính xác hơn và ngăn chặn xuyên qua tường
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, rb.velocity.y);
        tr.emitting = true;//vết đường color khi dash
        Instantiate(theThan, transform.position, transform.rotation);

        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    bool IsInSpecificStates(params string[] stateNames)
    {//các states cụ thể
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        foreach (string stateName in stateNames)
        {
            if (stateInfo.IsName(stateName))
                return true;
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public BoxCollider2D box;
    public float runSpeed = 7f;
    public float jumpSpeed = 12f;
    public LayerMask Ground;
    public bool faceingRight = true;
    public GameObject theThan;

    float move;
    bool canDash = true;
    bool isDashing;
    public float dashingPower = 300f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    public TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        //box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        animator.SetBool("Run", move != 0);
        flip();
        
        //nhảy
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
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
        if (Input.GetKeyDown(KeyCode.L) && canDash 
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk2")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk3")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk4")
            || Input.GetKeyDown(KeyCode.Keypad3) && canDash
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk2")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk3")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk4"))
        {
            StartCoroutine(Dash());
        }
        if (isDashing)
        {//ngăn player move,jump,.. khi dash
            return;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk1")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Player Hurt"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk2")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk3")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk4"))
        {
            rb.velocity = Vector2.zero;
        }
    }

    void flip()
    {
        if (move > 0 && !faceingRight
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk2")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk3")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk4")
            || move < 0 && faceingRight
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk2")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk3")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player atk4"))
        {
            /*faceingRight = !faceingRight;
            transform.Rotate(0, 180, 0);*/
            Vector3 localScale = transform.localScale;//
            faceingRight = !faceingRight;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D ray = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.1f, Ground);
        return ray.collider != null;
    }

    private IEnumerator Dash()
    {
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sr;
    RangeAtk3 r3; RangeAtk4 r4;
    PlayerController pctr;
    PlayerHealth pHP;
    SpellCooldown sc;

    public LayerMask enemyLayer;
    public Vector3 atkOffset1;
    public float atkRange1;//đường kính hình tròn range1
    public int atkDamage1 = 10;
    float nextAtkTime = 0;
    public Transform gunTip2, gunTip4, eye4;
    public GameObject bulletAtk2, ani1Atk3, ani2Atk3, ani3Atk3, bulletAtk4, eyeAtk4;
    private GameObject ani;
    [HideInInspector] public Transform targetE;
    [HideInInspector] public bool rangeAtk3 = false, rangeAtk4 = false;
    [HideInInspector] public float timeAtk1 = 0.75f, timeAtk2 = 1f, timeAtk3 = 1.5f, timeAtk4 = 1.5f;
    public bool useAtk3, useAtk4;
    bool clickAtk1, clickAtk2, clickAtk3, clickAtk4, performAtk;

    [Header("Sound atk")]
    public AudioSource atk1_audio;
    public AudioSource atk2_audio, atk3_audio, atk4_audio;

    [Header("MP Score")]
    public Slider MPSlider;
    public Text MPScore;

    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        r3 = GetComponentInChildren<RangeAtk3>();
        r4 = GetComponentInChildren<RangeAtk4>();
        pctr = GetComponent<PlayerController>();
        pHP = GetComponent<PlayerHealth>();
        sc = GetComponent<SpellCooldown>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAtkTime && !performAtk)
        {
            if ((Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Keypad1) || clickAtk1))
            {//lôi kiếm
                //Spell Cooldown//time hồi chiêu của skill
                sc.imageCooldown = sc.imageCooldown1;
                sc.cooldownTime = timeAtk1;
                sc.UseSpell();

                performAtk = true;//đang thực hiện atk
                Invoke("ResetPerformAtk", timeAtk1);
                nextAtkTime = Time.time + timeAtk1;
                animator.SetTrigger("Attack1");
                atk1_audio.Play();
            }

            else if ((Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Keypad2) || clickAtk2))
            {//hoả độn
                if (pHP.currentMP >= 2)
                {
                    sc.imageCooldown = sc.imageCooldown2;
                    sc.cooldownTime = timeAtk2;
                    sc.UseSpell();

                    performAtk = true;
                    Invoke("ResetPerformAtk", timeAtk2);
                    nextAtkTime = Time.time + timeAtk2;
                    animator.SetTrigger("Attack2");
                    atk2_audio.Play();

                    pHP.currentMP = pHP.currentMP - 2;
                    if (pHP.currentMP > pHP.maxMP) pHP.currentMP = pHP.maxMP;
                    MPScore.text = pHP.currentMP + " / " + pHP.maxMP;
                    MPSlider.value = pHP.currentMP;
                }
            }

            else if ((Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.Keypad4) || clickAtk3))
            {//chidori
                if (pHP.currentMP >= 3 && useAtk3 && rangeAtk3)
                {
                    sc.imageCooldown = sc.imageCooldown3;
                    sc.cooldownTime = timeAtk3;
                    sc.UseSpell();

                    performAtk = true;
                    Invoke("ResetPerformAtk", timeAtk3);
                    nextAtkTime = Time.time + timeAtk3;
                    animator.SetTrigger("Attack3");
                    atk3_audio.Play();

                    pHP.currentMP = pHP.currentMP - 3;
                    if (pHP.currentMP > pHP.maxMP) pHP.currentMP = pHP.maxMP;
                    MPScore.text = pHP.currentMP + " / " + pHP.maxMP;
                    MPSlider.value = pHP.currentMP;
                }
            }

            else if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Keypad5) || clickAtk4))
            {//Amaterasu
                if (pHP.currentMP >= 5 && useAtk4)
                {
                    sc.imageCooldown = sc.imageCooldown4;
                    sc.cooldownTime = timeAtk4;
                    sc.UseSpell();

                    performAtk = true;
                    Invoke("ResetPerformAtk", timeAtk3);
                    nextAtkTime = Time.time + timeAtk3;
                    animator.SetTrigger("Attack4");
                    atk4_audio.Play();

                    pHP.currentMP = pHP.currentMP - 5;
                    if (pHP.currentMP > pHP.maxMP) pHP.currentMP = pHP.maxMP;
                    MPScore.text = pHP.currentMP + " / " + pHP.maxMP;
                    MPSlider.value = pHP.currentMP;
                }
            }
        }
    }

    public void ClickAtk1()
    {
        if (!performAtk)
        {
            clickAtk1 = true;
            Invoke("ResetClickAtk1", timeAtk1);
        }
    }
    public void ClickAtk2()
    {
        if (!performAtk)
        {
            clickAtk2 = true;
            Invoke("ResetClickAtk2", timeAtk2);
        }
    }
    public void ClickAtk3()
    {
        if (!performAtk)
        {
            clickAtk3 = true;
            Invoke("ResetClickAtk3", timeAtk3);
        }
    }
    public void ClickAtk4()
    {
        if (!performAtk)
        {
            clickAtk4 = true;
            Invoke("ResetClickAtk4", timeAtk4);
        }
    }
    void ResetClickAtk1()
    {
        clickAtk1 = false;
    }
    void ResetClickAtk2()
    {
        clickAtk2 = false;
    }
    void ResetClickAtk3()
    {
        clickAtk3 = false;
    }
    void ResetClickAtk4()
    {
        clickAtk4 = false;
    }
    void ResetPerformAtk()
    {
        performAtk = false;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * atkOffset1.x * transform.localScale.x;
        pos += transform.up * atkOffset1.y;
        Gizmos.DrawWireSphere(pos, atkRange1);
    }

    void Attack1()
    {//lôi kiếm
        Vector3 pos = transform.position;
        pos += transform.right * atkOffset1.x * transform.localScale.x;
        pos += transform.up * atkOffset1.y;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(pos, atkRange1, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(atkDamage1);
        }
    }

    void Attack2()
    {//hoả độn
        if (pctr.faceingRight)
        {
            Instantiate(bulletAtk2, gunTip2.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        else if (!pctr.faceingRight)
        {
            Instantiate(bulletAtk2, gunTip2.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
    }

    void Attack3()
    {//chidori
        sr.enabled = false;
        Invoke("Attack3_4", 0.7f);
    }
    void Attack3_1()
    {
        if (pctr.faceingRight)
            ani = Instantiate(ani1Atk3, r3.newPos3_1, Quaternion.identity);
        else
            ani = Instantiate(ani1Atk3, r3.newPos3_1, Quaternion.Euler(new Vector3(0, 180, 0)));
        Destroy(ani, 1 / 6f);
    }
    void Attack3_2()
    {
        if (pctr.faceingRight)
            ani = Instantiate(ani2Atk3, r3.newPos3_2, Quaternion.identity);
        else
            ani = Instantiate(ani2Atk3, r3.newPos3_2, Quaternion.Euler(new Vector3(0, 180, 0)));
        Destroy(ani, 1 / 6f);
    }
    void Attack3_3()
    {
        if (pctr.faceingRight)
            ani = Instantiate(ani3Atk3, r3.newPos3_3, Quaternion.identity);
        else
            ani = Instantiate(ani3Atk3, r3.newPos3_3, Quaternion.Euler(new Vector3(0, 180, 0)));
        Destroy(ani, 0.2f);
        Invoke("Attack3_4", 0.2f);
    }
    void Attack3_4()
    {
        sr.enabled = true;
    }

    void Attack4()
    {//chidori
        Instantiate(eyeAtk4, eye4.position, Quaternion.identity);
        if (!rangeAtk4)
        {
            targetE = gunTip4;
            Instantiate(bulletAtk4, targetE.position, Quaternion.identity);
        }
        else
            Instantiate(bulletAtk4, r4.newPosition, Quaternion.identity);
    }
}

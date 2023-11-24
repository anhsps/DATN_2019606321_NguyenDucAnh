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

    public LayerMask enemyLayer;
    public Vector3 atkOffset1;
    public float atkRange1;
    public int atkDamage1 = 10;
    float nextAtkTime = 0;
    public Transform gunTip2, gunTip4, eye4;
    public GameObject bulletAtk2, ani1Atk3, ani2Atk3, ani3Atk3, bulletAtk4, eyeAtk4;
    private GameObject ani;
    [HideInInspector] public Transform targetE;
    public bool rangeAtk3 = false, rangeAtk4 = false;
    public bool useAtk3, useAtk4;

    /*[Header("Sound atk")]
    public AudioSource atk1_audio;
    public AudioSource atk2_audio, atk3_audio, atk4_audio;*/

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAtkTime)
        {
            if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Keypad1))
            {//lôi kiếm
                nextAtkTime = Time.time + 0.75f;
                animator.SetTrigger("Attack1");
                //atk1_audio.Play();
            }

            else if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Keypad2))
            {//hoả độn
                if (pHP.currentMP >= 2)
                {
                    nextAtkTime = Time.time + 1f;
                    animator.SetTrigger("Attack2");
                    //atk2_audio.Play();

                    pHP.currentMP = pHP.currentMP - 2;
                    if (pHP.currentMP > pHP.maxMP) pHP.currentMP = pHP.maxMP;
                    MPScore.text = pHP.currentMP + " / " + pHP.maxMP;
                    MPSlider.value = pHP.currentMP;
                }
            }

            else if (Input.GetKeyDown(KeyCode.U) && rangeAtk3 && useAtk3
                || Input.GetKeyDown(KeyCode.Keypad4) && rangeAtk3 && useAtk3)
            {//chidori
                if (pHP.currentMP >= 3)
                {
                    nextAtkTime = Time.time + 1.5f;
                    animator.SetTrigger("Attack3");
                    //atk3_audio.Play();

                    pHP.currentMP = pHP.currentMP - 3;
                    if (pHP.currentMP > pHP.maxMP) pHP.currentMP = pHP.maxMP;
                    MPScore.text = pHP.currentMP + " / " + pHP.maxMP;
                    MPSlider.value = pHP.currentMP;
                }
            }

            else if (Input.GetKeyDown(KeyCode.I) && useAtk4 || Input.GetKeyDown(KeyCode.Keypad5) && useAtk4)
            {//Amaterasu
                if (pHP.currentMP >= 5)
                {
                    nextAtkTime = Time.time + 1.5f;
                    animator.SetTrigger("Attack4");
                    //atk4_audio.Play();

                    pHP.currentMP = pHP.currentMP - 5;
                    if (pHP.currentMP > pHP.maxMP) pHP.currentMP = pHP.maxMP;
                    MPScore.text = pHP.currentMP + " / " + pHP.maxMP;
                    MPSlider.value = pHP.currentMP;
                }
            }
        }
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
        else if(!pctr.faceingRight)
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

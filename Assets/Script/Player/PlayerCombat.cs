using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sr;
    private RangeAtk3 r3; RangeAtk4 r4;
    private PlayerController pctr;
    private PlayerHealth pH;
    private SpellCooldown sc;
    private GameObject ani;
    private bool clickAtk1, clickAtk2, clickAtk3, clickAtk4, performAtk;

    public LayerMask enemyLayer;
    public Vector3 pointAtk1;
    public float rangeAtk1 = 2;//đường kính hình tròn range atk1
    public int damageAtk1 = 10;
    public Transform gunTip2, gunTip4, eye4;
    public GameObject bulletAtk2, ani1Atk3, ani2Atk3, ani3Atk3, bulletAtk4, eyeAtk4;
    [HideInInspector] public float timeAtk1 = 0.75f, timeAtk2 = 1f, timeAtk3 = 1.5f, timeAtk4 = 1.5f;
    [HideInInspector] public bool rangeAtk3, rangeAtk4;
    public bool useAtk3, useAtk4;

    [Header("Sound atk")]
    public AudioSource atk1_audio;
    public AudioSource atk2_audio, atk3_audio, atk4_audio;

    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        r3 = GetComponentInChildren<RangeAtk3>();
        r4 = GetComponentInChildren<RangeAtk4>();
        pctr = GetComponent<PlayerController>();
        pH = GetComponent<PlayerHealth>();
        sc = GetComponent<SpellCooldown>();//time hồi chiêu của skill
    }

    void Update()
    {
        if (!performAtk)
        {
            if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Keypad1) || clickAtk1)
            {//lôi kiếm
                AttackSettings(sc.imageCooldown1, timeAtk1, "Attack1", atk1_audio, 0);
            }

            else if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Keypad2) || clickAtk2)
            {//hoả độn
                if (pH.currentMP >= 2)
                    AttackSettings(sc.imageCooldown2, timeAtk2, "Attack2", atk2_audio, 2);
            }

            else if (Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.Keypad4) || clickAtk3)
            {//chidori
                if (pH.currentMP >= 3 && useAtk3)
                    AttackSettings(sc.imageCooldown3, timeAtk3, "Attack3", atk3_audio, 3);
            }

            else if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Keypad5) || clickAtk4)
            {//Amaterasu
                if (pH.currentMP >= 5 && useAtk4)
                    AttackSettings(sc.imageCooldown4, timeAtk4, "Attack4", atk4_audio, 5);
            }
        }
    }

    private void AttackSettings(Image image, float timeAtk, string Attack, AudioSource audio, int amountMP)
    {
        sc.imageCooldown = image;
        sc.cooldownTime = timeAtk;
        sc.UseSpell();

        performAtk = true;//đang thực hiện atk
        Invoke("ResetPerformAtk", timeAtk);
        animator.SetTrigger(Attack);
        audio.Play();

        pH.currentMP -= amountMP;
        if (pH.currentMP > pH.maxMP) pH.currentMP = pH.maxMP;
        pH.MPScore.text = pH.currentMP + " / " + pH.maxMP;
        pH.SliderMP.value = pH.currentMP;
    }

    public void ClickAtk1() { ClickAtk(ref clickAtk1, timeAtk1); }
    public void ClickAtk2() { ClickAtk(ref clickAtk2, timeAtk2); }
    public void ClickAtk3() { ClickAtk(ref clickAtk3, timeAtk3); }
    public void ClickAtk4() { ClickAtk(ref clickAtk4, timeAtk4); }
    void ClickAtk(ref bool clickAtk, float timeAtk)
    {
        if (!performAtk)
        {
            clickAtk = true;
            Invoke("ResetClickAtk", timeAtk);
        }
    }
    void ResetClickAtk() { clickAtk1 = clickAtk2 = clickAtk3 = clickAtk4 = false; }
    void ResetPerformAtk() { performAtk = false; }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * pointAtk1.x * transform.localScale.x;
        pos += transform.up * pointAtk1.y;
        Gizmos.DrawWireSphere(pos, rangeAtk1);
    }
    //lôi kiếm
    void Attack1()
    {
        Vector3 pos = transform.position;
        pos += transform.right * pointAtk1.x * transform.localScale.x;
        pos += transform.up * pointAtk1.y;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(pos, rangeAtk1, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damageAtk1);
        }
    }
    //hoả độn
    void Attack2() { CheckFace(bulletAtk2, gunTip2.position, 0.5f); }
    //chidori
    void Attack3()
    {
        sr.enabled = false;
        Invoke("Attack3_4", 0.7f);
        if (!rangeAtk3)
        {
            r3.newPos3_3 = new Vector2(gunTip4.position.x, transform.position.y);
            r3.newPos3_1 = new Vector2(transform.position.x + (r3.newPos3_3.x - transform.position.x) / 3f, transform.position.y);
            r3.newPos3_2 = new Vector2(transform.position.x + (r3.newPos3_3.x - transform.position.x) * 2 / 3f, transform.position.y);
        }
    }
    void Attack3_1() { CheckFace(ani1Atk3, r3.newPos3_1, 1 / 6f); }
    void Attack3_2() { CheckFace(ani2Atk3, r3.newPos3_2, 1 / 6f); }
    void Attack3_3() { CheckFace(ani3Atk3, r3.newPos3_3, 0.2f); }
    void Attack3_4() { sr.enabled = true; }

    void CheckFace(GameObject prefab, Vector2 pos, float t)
    {
        if (pctr.faceingRight)
            ani = Instantiate(prefab, pos, Quaternion.identity);
        else
            ani = Instantiate(prefab, pos, Quaternion.Euler(new Vector3(0, 180, 0)));
        Destroy(ani, t);
    }

    void Attack4()
    {//Amaterasu
        Instantiate(eyeAtk4, eye4.position, Quaternion.identity);
        if (rangeAtk4)
            Instantiate(bulletAtk4, r4.newPosition, Quaternion.identity);
        else
            Instantiate(bulletAtk4, gunTip4.position, Quaternion.identity);
    }
}

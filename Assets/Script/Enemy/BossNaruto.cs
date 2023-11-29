using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//điều kiện chạy các anim, khoảng cách atk enemy, move...
public class BossNaruto : MonoBehaviour
{
    #region Public Variables
    public int atkA = 1, atkB1 = 3, atkB2 = 2, atkB3 = 4;//kiểu atk thứ . 1 <= atkA <= atkB
    public float atkDistance1_1, atkDistance2_1, atkDistance3_1, atkDistance3_2, atkDistanceBullet;//
    public float moveSpeed;
    public float timer;//time hồi chiêu
    public Transform leftLimit, rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;//phạm vi
    public Transform CheckRange;//vùng move vs atk
    public bool isFlipped = true;
    #endregion

    #region Private Variables
    EnemyHealth e_HP;
    Animator animator;
    float distance;
    bool atkMode;//true thì atk, false thì move
    bool cooling;
    float intTimer;//lưu trữ gt ban đầu của bộ đếm thời gian
    #endregion

    public bool form2, form3;//
    public GameObject bullet1_2, bullet1_3;
    public Transform pos1_2, pos1_3;

    // Start is called before the first frame update
    void Awake()
    {
        intTimer = timer;
        animator = GetComponent<Animator>();
        e_HP = GetComponent<EnemyHealth>();
        SelectTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (e_HP.currentHP <= 0.7 * e_HP.maxHP)//
        {
            form2 = true;
            animator.SetTrigger("Form2");
            e_HP.hurtPrefix = "Hurt2";
        }
        if (e_HP.currentHP <= 0.5 * e_HP.maxHP)//
        {
            form2 = false;
            form3 = true;
            animator.SetTrigger("Form3");
            e_HP.hurtPrefix = "Hurt3";
        }
        if (e_HP.currentHP <= 0)
        {
            this.enabled = false;
        }

        if (!atkMode)
        {
            Move();
        }

        if (!InsideofLimits() && !inRange)
        {
            SelectTarget();
        }

        if (inRange)
        {
            EnemyLogic();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (!form2 && !form3)
        {
            if (distance > atkDistanceBullet)
            {
                StopAttack();
            }
            else if (distance <= atkDistance1_1 && !cooling)
            {
                animator.SetBool("Attack1_" + Random.Range(1, atkB1 + 1), true);
                AttackAnimation();
            }
            else if (distance <= atkDistanceBullet && !cooling)
            {
                animator.SetBool("Attack1_" + Random.Range(2, atkB1 + 1), true);
                AttackAnimation();
            }
        }

        else if (form2)
        {
            if (distance > atkDistanceBullet)
            {
                StopAttack();
            }
            else if (distance <= atkDistance2_1 && !cooling)
            {
                animator.SetBool("Attack2_" + Random.Range(1, atkB2 + 1), true);
                AttackAnimation();
            }
            else if (distance <= atkDistanceBullet && !cooling)
            {
                animator.SetBool("Attack2_" + Random.Range(2, atkB2 + 1), true);
                AttackAnimation();
            }
        }

        else if (form3)
        {
            if (distance > atkDistanceBullet)
            {
                StopAttack();
            }
            else if (distance <= atkDistance3_1 && !cooling)
            {
                animator.SetBool("Attack3_" + Random.Range(1, atkB3 + 1), true);
                AttackAnimation();
            }
            else if (distance <= atkDistance3_2 && !cooling)
            {
                animator.SetBool("Attack3_" + Random.Range(2, atkB3 + 1), true);
                AttackAnimation();
            }
            else if (distance <= atkDistanceBullet && !cooling)
            {
                animator.SetBool("Attack3_" + Random.Range(3, atkB1 + 1), true);
                AttackAnimation();
            }
        }

        if (cooling)
        {
            Cooldown();
            ResetAtkBoolValues();
        }
    }

    void Move()
    {
        string RunPrefix = form2 ? "Run2" : (form3 ? "Run3" : "Run");
        animator.SetBool(RunPrefix, true);

        string[] listStates = { "atk1_1", "atk1_2", "atk1_3", "atk2_1", "atk2_2","atk3_1", "atk3_2", "atk3_3", "atk3_4",
            "hurt", "hurt2", "hurt3" };
        if (!IsInSpecificStates(listStates))
        {
            Vector2 TargetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void AttackAnimation()
    {
        string RunPrefix = form2 ? "Run2" : (form3 ? "Run3" : "Run");
        animator.SetBool(RunPrefix, false);
        timer = intTimer;//reset timer when player enter attack range
        atkMode = true;
    }

    void ResetAtkBoolValues()
    {
        int atkB = form2 ? atkB2 : (form3 ? atkB3 : atkB1);
        string attackPrefix = form2 ? "Attack2_" : (form3 ? "Attack3_" : "Attack1_");
        for (int i = atkA; i <= atkB; i++)
        {
            string boolName = attackPrefix + i;
            animator.SetBool(boolName, false);
        }
    }

    void Cooldown()
    {//reset timer atk
        timer -= Time.deltaTime;
        if (timer <= 0 && cooling && atkMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        atkMode = false;
        ResetAtkBoolValues();
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    public bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }
        Flip();
    }

    void BulletAtk()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("atk1_2"))
        {
            if (isFlipped)
                Instantiate(bullet1_2, pos1_2.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            else
                Instantiate(bullet1_2, pos1_2.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("atk1_3"))
        {
            if (isFlipped)
                Instantiate(bullet1_3, pos1_3.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            else
                Instantiate(bullet1_3, pos1_3.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("atk2_2"))
        {
            if (isFlipped)
                Instantiate(bullet1_3, pos1_3.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            else
                Instantiate(bullet1_3, pos1_3.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("atk3_3"))
        {
            if (isFlipped)
                Instantiate(bullet1_3, pos1_3.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            else
                Instantiate(bullet1_3, pos1_3.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("atk3_4"))
        {
            if (isFlipped)
                Instantiate(bullet1_3, pos1_3.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            else
                Instantiate(bullet1_3, pos1_3.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
    }

    public void Flip()
    {
        bool facingLeft = transform.position.x > target.position.x;
        string[] listStates = { "atk1_1", "atk1_2", "atk1_3", "atk2_1", "atk2_2","atk3_1", "atk3_2", "atk3_3", "atk3_4",
            "hurt", "hurt2", "hurt3", "naruto form2", "naruto form3" };
        if (!IsInSpecificStates(listStates))
        {
            if (facingLeft && isFlipped || !facingLeft && !isFlipped)
            {
                transform.Rotate(0, 180, 0);
                isFlipped = !isFlipped;
            }
        }
    }

    bool IsInSpecificStates(params string[] stateNames)
    {//các states cụ thể
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        foreach (string stateName in stateNames)
        {
            if (stateInfo.IsName(stateName))
            {
                return true;
            }
        }
        return false;
    }
}

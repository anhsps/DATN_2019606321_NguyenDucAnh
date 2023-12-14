using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//điều kiện chạy các anim, khoảng cách atk enemy, move...
public class EnemyBehavior : MonoBehaviour
{
    #region Public Variables
    public int atkA, atkB;//kiểu atk thứ . 1 <= atkA <= atkB
    public float atkDistance, atkDistance2;//
    public float moveSpeed;
    public float timer;//time hồi chiêu
    public Transform leftLimit, rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;//dk phạm vi atk
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

        if (distance > atkDistance2 || (atkA == 1 && atkB == 1 && distance > atkDistance))//
        {
            StopAttack();
        }
        else if (distance <= atkDistance && cooling == false)
        {
            animator.SetBool("Attack" + Random.Range(1, atkB + 1), true);
            AttackAnimation();
        }
        else if (distance <= atkDistance2 && cooling == false)//
        {//nếu ko atk1, mà từ atkA=2 trở đi thì cài atkDistance=0
            int atkAStart = (atkA > 1) ? atkA : 2;
            animator.SetBool($"Attack{Random.Range(atkAStart, atkB + 1)}", true);
            /*if (atkA > 1) animator.SetBool("Attack" + Random.Range(atkA, atkB + 1), true);
            animator.SetBool("Attack" + Random.Range(2, atkB + 1), true);*/
            AttackAnimation();
        }

        if (cooling)
        {
            Cooldown();
            ResetAtkBoolValues();
        }
    }

    void Move()
    {
        string[] listStates = { "atk1", "atk2", "atk3", "atk4", "hurt" };
        animator.SetBool("Run", true);
        if (!IsInSpecificStates(listStates))
        {
            Vector2 TargetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void AttackAnimation()
    {
        //animator.SetBool("Attack" + Random.Range(1, atkType + 1), true);
        animator.SetBool("Run", false);
        timer = intTimer;//reset timer when player enter attack range
        atkMode = true;
    }

    void ResetAtkBoolValues()
    {
        for (int i = atkA; i <= atkB; i++)
        {
            string boolName = "Attack" + i;
            animator.SetBool(boolName, false);
        }
    }

    void Cooldown()
    {//reset timer atk
        timer -= Time.deltaTime;
        if (!inRange && timer > 0) timer = intTimer;//fix lỗi !inRange mà timer chưa trừ hết về 0
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
        ResetAtkBoolValues();//
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    bool InsideofLimits()
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

    public void Flip()
    {
        bool facingLeft = transform.position.x > target.position.x;
        string[] listStates = { "atk1", "atk2", "atk3", "atk4", "hurt" };
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

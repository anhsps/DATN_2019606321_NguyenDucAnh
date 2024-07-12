using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//điều kiện chạy các anim, khoảng cách atk enemy, move...
public class EnemyBehavior : MonoBehaviour
{
    #region Public Variables
    public int atkA, atkB;//kiểu atk thứ . 1 <= atkA <= atkB
    public float atkDistance, atkDistance2;
    public float moveSpeed;
    public float timer;////time hồi chiêu giữa các cuộc atk
    public Transform leftLimit, rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;//dk phạm vi atk
    [HideInInspector] public bool isFlipped = true;
    #endregion

    #region Private Variables
    EnemyHealth e_HP;
    Animator animator;
    float distance;
    float intTimer;//lưu trữ gt ban đầu của bộ đếm thời gian
    bool atkMode;//true là đang atk, false thì move
    bool cooling;
    string[] listStates = { "atk1", "atk2", "atk3", "atk4", "hurt" };
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
        Flip();
        if (e_HP.currentHP <= 0)
            this.enabled = false;

        if (!atkMode)
            Move();

        if (!InsideOfLimits())
            SelectTarget();

        if (inRange && target != leftLimit && target != rightLimit && !cooling)
            EnemyLogic();

        if (cooling)
        {
            Cooldown();
            ResetAtkBoolValues();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > atkDistance2 || (distance > atkDistance && atkA == 1 && atkB == 1))
            StopAttack();
        else if (distance <= atkDistance)
            AttackAnimation(1, atkB);
        else if (distance <= atkDistance2)//
        {//nếu ko atk1, mà từ atkA=2 trở đi thì cài atkDistance=0
            int atkAStart = (atkA > 1) ? atkA : 2;
            AttackAnimation(atkAStart, atkB);
        }
    }

    void Move()
    {
        animator.SetBool("Run", true);
        if (!IsInSpecificStates(listStates))
        {
            Vector2 TargetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void AttackAnimation(int atkA, int atkB)
    {
        atkMode = true;
        animator.SetBool("Run", false);
        animator.SetBool($"Attack{Random.Range(atkA, atkB + 1)}", true);
        //animator.SetBool("Attack" + Random.Range(atkA, atkB + 1), true);
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
        if (timer <= 0)
        {
            timer = intTimer;//reset timer when player enter attack range
            StopAttack();
        }
    }

    void StopAttack()
    {
        cooling = false;
        atkMode = false;
    }

    bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void TriggerCooling() { cooling = true; }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
            target = leftLimit;
        else
            target = rightLimit;

        #region c2
        /*if (transform.position == leftLimit.position)
            target = rightLimit;
        if (transform.position == rightLimit.position)
            target = leftLimit;*/
        #endregion
    }

    public void Flip()
    {
        bool facingLeft = transform.position.x > target.position.x;
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
                return true;
        }
        return false;
    }
}

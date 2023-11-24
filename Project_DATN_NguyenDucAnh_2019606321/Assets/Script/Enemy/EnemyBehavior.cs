using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//điều kiện chạy các anim, khoảng cách atk enemy, move...
public class EnemyBehavior : MonoBehaviour
{
    #region Public Variables
    public int atkA, atkB;//kiểu atk thứ
    public float atkDistance, atkDistance2;//
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

        if (distance > atkDistance2)//
        {
            StopAttack();
        }
        else if(atkA == 1 && atkB==1 && distance > atkDistance)//
        {
            StopAttack();
        }
        else if (distance <= atkDistance && cooling == false)
        {
            animator.SetBool("Attack" + Random.Range(1, atkB + 1), true);
            AttackAnimation();
        }
        else if (distance <= atkDistance2 && cooling == false)//
        {
            animator.SetBool("Attack" + Random.Range(2, atkB + 1), true);
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
        animator.SetBool("Run", true);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("atk1")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("atk2")//
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("atk3")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("atk4")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
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
        for (int i = atkA; i <= atkB; i++)//
        {
            string boolName = "Attack" + i;
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
        if (transform.position.x > target.position.x && isFlipped
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("atk1")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("atk2")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("atk3")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("atk4")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
        {
            transform.Rotate(0, 180, 0);
            isFlipped = false;
        }
        else if(transform.position.x < target.position.x && !isFlipped
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("atk1")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("atk2")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("atk3")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("atk4")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
        {
            transform.Rotate(0, 180, 0);
            isFlipped = true;
        }
    }
}

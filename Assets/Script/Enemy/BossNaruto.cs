using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//điều kiện chạy các anim, khoảng cách atk boss, move...
public class BossNaruto : MonoBehaviour
{
    #region Public Variables
    public float atkDistance1_1, atkDistance2_1, atkDistance3_1, atkDistance3_2, atkDistanceBullet;
    public float moveSpeed;
    public float timer;//time hồi chiêu
    public Transform leftLimit, rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;//phạm vi
    public Transform CheckRange;//vùng move vs atk
    [HideInInspector] public bool isFlipped = true;
    public GameObject bullet1_2, bullet1_3, bullet2_2;
    public Transform pos1_2, pos1_3, pos2_2, pos3_3, pos3_4;
    public AudioSource rsg_audio, rssrk_audio, smoke_audio, atk1_1_audio, atk1_2_audio, naForm2_audio, naForm3_audio;
    #endregion

    #region Private Variables
    Animator animator;
    EnemyHealth e_HP;
    EnemyAtk1 eAtk1;
    float distance;
    bool atkMode;//true thì atk, false thì move
    bool cooling;
    float intTimer;//lưu trữ gt ban đầu của bộ đếm thời gian
    int atkA = 1, atkB1 = 3, atkB2 = 2, atkB3 = 4;//kiểu atk thứ . 1 <= atkA <= atkB
    bool form2, form3;//trạng thái (or dạng biến hình mới) của boss
    string[] listStates = { "atk1_1", "atk1_2", "atk1_3", "atk2_1", "atk2_2","atk3_1", "atk3_2", "atk3_3", "atk3_4",
            "hurt", "hurt2", "hurt3", "naruto form2", "naruto form3" };
    string originalTag;
    int originalLayer;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        intTimer = timer;
        animator = GetComponent<Animator>();
        e_HP = GetComponent<EnemyHealth>();
        eAtk1 = GetComponent<EnemyAtk1>();
        SelectTarget();

        originalTag = gameObject.tag;
        originalLayer = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        string[] listStates1 = { "hurt", "atk1_1", "atk1_2", "atk1_3" };
        string[] listStates2 = { "hurt2", "atk2_1", "atk2_2" };
        //string[] listStatesform = { "naruto form2", "naruto form3" };
        if (e_HP.currentHP <= 0.7 * e_HP.maxHP && !IsInSpecificStates(listStates1))
        {
            form2 = true;
            animator.SetTrigger("Form2");
            e_HP.hurtPrefix = "Hurt2";
        }
        if (e_HP.currentHP <= 0.5 * e_HP.maxHP && !IsInSpecificStates(listStates2))
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
        if (IsInSpecificStates("naruto form2") || IsInSpecificStates("naruto form3"))
        {//Boss chạy animator naruto form2 3 thì ẩn tag,layer rồi bật lại sau 1.5s (bất tử 1.5s biến hình)
            gameObject.tag = "Untagged";
            gameObject.layer = 0;
            Invoke("ShowTagLayer", 1.5f);
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

        eAtk1.atkDamage = (IsInSpecificStates("atk1_1")) ? 20 : 10;
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
                animator.SetBool("Attack3_" + Random.Range(3, atkB3 + 1), true);
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
        Vector3 bulletRotation = isFlipped ? Vector3.zero : new Vector3(0, 180, 0);
        if (IsInSpecificStates("atk1_2"))
        {
            Instantiate(bullet1_2, pos1_2.position, Quaternion.Euler(bulletRotation));
        }
        else if (IsInSpecificStates("atk1_3"))
        {
            Instantiate(bullet1_3, pos1_3.position, Quaternion.Euler(bulletRotation));
        }

        else if (IsInSpecificStates("atk2_2"))
        {
            Instantiate(bullet2_2, pos2_2.position, Quaternion.Euler(bulletRotation));
        }

        else if (IsInSpecificStates("atk3_3"))
        {
            Instantiate(bullet1_2, pos3_3.position, Quaternion.Euler(bulletRotation));
        }
        else if (IsInSpecificStates("atk3_4"))
        {
            Instantiate(bullet2_2, pos3_4.position, Quaternion.Euler(bulletRotation));
        }
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
            {
                return true;
            }
        }
        return false;
    }

    void ShowTagLayer()
    {//bật lại tag vs layer gt ban đầu
        gameObject.tag = originalTag;
        gameObject.layer = originalLayer;
    }

    void SoundNaruto()
    {
        if (IsInSpecificStates("atk1_1", "atk2_2", "atk3_4")) rsg_audio.Play();
        else if (IsInSpecificStates("atk1_2", "atk3_3")) rssrk_audio.Play();
        else if (IsInSpecificStates("atk1_3")) smoke_audio.Play();
        else if (IsInSpecificStates("atk2_1", "atk3_2")) atk1_2_audio.Play();
        else if (IsInSpecificStates("atk3_1")) atk1_1_audio.Play();

        else if (IsInSpecificStates("naruto form2")) naForm2_audio.Play();
        else if (IsInSpecificStates("naruto form3")) naForm3_audio.Play();

        /*if (!IsInSpecificStates(listStates))
        {
            AudioSource[] audiosStop = { rsg_audio, rssrk_audio, smoke_audio, atk1_1_audio, atk1_2_audio, naForm2_audio, naForm3_audio };
            foreach (AudioSource audioSource in audiosStop)
            {
                audioSource.Stop();
            }
        }*/
    }
}

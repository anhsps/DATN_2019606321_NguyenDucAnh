using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossItachi : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sr;
    EnemyBehavior bParent;
    public GameObject bullet2, bullet3,bullet4;
    public Transform pos2;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        bParent = GetComponent<EnemyBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BulletAtk()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("atk2"))
        {
            if (bParent.isFlipped)
                Instantiate(bullet2, pos2.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            else
                Instantiate(bullet2, pos2.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("atk3"))
        {//bullet bắn target vào player
            if (bParent.isFlipped)
                Instantiate(bullet3, bParent.target.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            else
                Instantiate(bullet3, bParent.target.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("atk4"))
        {
            sr.enabled = false;
            Invoke("BulletAtk4_2", 0.5f);
            if (bParent.isFlipped)
                Instantiate(bullet4, bParent.target.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            else
                Instantiate(bullet4, bParent.target.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
    }

    void BulletAtk4_2()
    {
        sr.enabled = true;
    }
}

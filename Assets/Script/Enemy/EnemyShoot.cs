using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    EnemyBehavior eParent;
    Animator animator;
    public GameObject bullet2, bullet3;
    public Transform pos2, pos3;
    public bool bulletAtk2, bulletAtk3, bulletAtk3_2;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        eParent = GetComponent<EnemyBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BulletAtk2()
    {
        if (bulletAtk2 && animator.GetCurrentAnimatorStateInfo(0).IsName("atk2"))
        {
            if (eParent.isFlipped)
                Instantiate(bullet2, pos2.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            else
                Instantiate(bullet2, pos2.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }

        else if (bulletAtk3 && animator.GetCurrentAnimatorStateInfo(0).IsName("atk3"))
        {
            if (eParent.isFlipped)
                Instantiate(bullet3, pos3.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            else
                Instantiate(bullet3, pos3.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }

        else if(bulletAtk3_2 && animator.GetCurrentAnimatorStateInfo(0).IsName("atk3"))
        {//bullet bắn thẳng player
            if (eParent.isFlipped)
                Instantiate(bullet3, eParent.target.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            else
                Instantiate(bullet3, eParent.target.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
    }
}

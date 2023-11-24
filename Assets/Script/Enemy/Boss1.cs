using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : EnemyShoot
{
    Animator ani;
    EnemyBehavior bParent;
    SpriteRenderer sr;
    public GameObject bullet4;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        bParent = GetComponent<EnemyBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BulletAtk4()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("atk4"))
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

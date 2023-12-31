﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanzoBullet : MonoBehaviour
{
    EnemyBehavior eParent;
    Animator animator;
    public GameObject bullet3, bullet3_2;
    public Transform pos3, pos3_2;

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

    void BulletAtk3()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("atk3"))
        {
            if (eParent.isFlipped)
                Instantiate(bullet3, pos3.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            else
                Instantiate(bullet3, pos3.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
        Invoke("BulletAtk3_2", 0.5f);
    }
    void BulletAtk3_2()
    {
        if (eParent.isFlipped)
            Instantiate(bullet3_2, pos3_2.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        else
            Instantiate(bullet3_2, pos3_2.position, Quaternion.Euler(new Vector3(0, 180, 0)));
    }
}

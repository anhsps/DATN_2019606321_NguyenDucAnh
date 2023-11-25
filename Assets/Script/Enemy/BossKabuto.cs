using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKabuto : MonoBehaviour
{
    EnemyBehavior bParent;
    public GameObject bullet2, bullet3, bullet4_1, bullet4_2;
    public Transform pos2_1, pos2_2, pos3, pos4_1, pos4_2;

    // Start is called before the first frame update
    void Start()
    {
        bParent = GetComponent<EnemyBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void BulletAtk2_1()
    {
        Instantiate(bullet2, pos2_1.position, Quaternion.identity);
    }
    void BulletAtk2_2()
    {
        Instantiate(bullet2, pos2_2.position, Quaternion.identity);
    }
    void BulletAtk3()
    {
        if (bParent.isFlipped)
            Instantiate(bullet3, pos3.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        else
            Instantiate(bullet3, pos3.position, Quaternion.Euler(new Vector3(0, 180, 0)));
    }
    void BulletAtk4_1()
    {
        Instantiate(bullet4_1, pos4_1.position, Quaternion.identity);
    }
    void BulletAtk4_2()
    {
        Instantiate(bullet4_2, pos4_2.position, Quaternion.identity);
    }
}

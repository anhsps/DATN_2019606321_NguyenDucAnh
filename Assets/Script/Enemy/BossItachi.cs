using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossItachi : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sr;
    EnemyBehavior bParent;
    public GameObject bullet2, bullet3, bullet4;
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
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Vector3 bulletRotation = (bParent.isFlipped) ? Vector3.zero : new Vector3(0, 180, 0);
        if (stateInfo.IsName("atk2"))
            Instantiate(bullet2, pos2.position, Quaternion.Euler(bulletRotation));
        //bullet bắn target vào player
        else if (stateInfo.IsName("atk3"))
            Instantiate(bullet3, bParent.target.position, Quaternion.Euler(bulletRotation));
        else if (stateInfo.IsName("atk4"))
        {
            sr.enabled = false;
            Invoke("BulletAtk4_2", 0.5f);
            Instantiate(bullet4, bParent.target.position, Quaternion.Euler(bulletRotation));
        }
    }

    void BulletAtk4_2() { sr.enabled = true; }
}

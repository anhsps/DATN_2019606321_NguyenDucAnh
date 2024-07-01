using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    Animator animator;
    EnemyBehavior eParent;
    public GameObject bullet2, bullet3;
    public Transform pos2, pos3;

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

    void BulletAtk()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Vector3 bulletRotation = (eParent.isFlipped) ? Vector3.zero : new Vector3(0, 180, 0);
        if (stateInfo.IsName("atk2"))
            Instantiate(bullet2, pos2.position, Quaternion.Euler(bulletRotation));
        else if (stateInfo.IsName("atk3"))
            Instantiate(bullet3, pos3.position, Quaternion.Euler(bulletRotation));
    }
}

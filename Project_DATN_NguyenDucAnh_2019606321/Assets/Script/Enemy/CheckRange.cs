using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRange : MonoBehaviour
{
    EnemyBehavior eParent;
    EnemyHealth e_hp;

    // Start is called before the first frame update
    void Start()
    {
        eParent = GetComponentInParent<EnemyBehavior>();
        e_hp = GetComponentInParent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (eParent.inRange && e_hp.currentHP > 0)
            eParent.Flip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            eParent.inRange = true;
            eParent.target = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            eParent.inRange = false;
        }
    }
}

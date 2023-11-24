using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBulletDamage : MonoBehaviour
{
    public int damage = 20;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }
    }
}

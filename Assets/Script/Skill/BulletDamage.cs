using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                //GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}

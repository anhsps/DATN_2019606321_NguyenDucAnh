using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage2 : MonoBehaviour
{
    public int damage = 20;

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
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}

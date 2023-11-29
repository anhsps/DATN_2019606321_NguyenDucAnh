using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damage = 10;
    public BoxCollider2D boxE, boxP;
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
        if (collision.gameObject.CompareTag("Enemy") && boxE != null)
        {
            //if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))//k xài
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (boxP != null && collision.gameObject.GetComponent<PlayerHealth>().isHurt)
            {// Nếu Player bị hurt và là boxP
                Destroy(gameObject);
            }
        }
    }
}

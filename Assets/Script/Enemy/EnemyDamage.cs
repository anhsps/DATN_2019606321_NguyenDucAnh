using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 10;
    public float damRate = 1f;//time gây đam 1 lần
    float nextDamage = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")  && nextDamage < Time.time)
        {
            PlayerHealth p_HP = collision.gameObject.GetComponent<PlayerHealth>();
            p_HP.TakeDamage(damage);
            nextDamage = damRate + Time.time;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBulletController : MonoBehaviour
{
    Rigidbody2D rb;
    public float eBulletSpeed = 6f;
    public int damage = 20;
    public float desTime = 0.2f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (transform.localRotation.y > 0)
            rb.AddForce(new Vector2(-1, 0) * eBulletSpeed, ForceMode2D.Impulse);
        else
            rb.AddForce(new Vector2(1, 0) * eBulletSpeed, ForceMode2D.Impulse);
        //ForceMode2D.Impulse: xung lực vô vật rắn hay thêm ngay 1 lực làm cho vật lập tức bay đi
    }

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
                Destroy(gameObject, desTime);
            }
        }
    }
}

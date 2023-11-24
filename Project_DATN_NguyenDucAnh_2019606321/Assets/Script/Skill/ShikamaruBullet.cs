using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShikamaruBullet : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    public float eBulletSpeed = 6f;
    public GameObject bullet2_2;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
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
                //collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                sr.enabled = false;              
            }
            Vector2 target = new Vector2(collision.transform.position.x - 0.25f, transform.position.y);
            Instantiate(bullet2_2, target, Quaternion.identity);
        }
    }
}

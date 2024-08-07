﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBulletController : MonoBehaviour
{
    Rigidbody2D rb;
    public int damage = 20;
    public float eBulletSpeed = 6f;
    public float desTime = 0.1f;
    float damRate = 1f;
    float nextDamge;
    
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && nextDamge < Time.time)
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            nextDamge = damRate + Time.time;
            Destroy(gameObject, desTime);
        }
    }
}

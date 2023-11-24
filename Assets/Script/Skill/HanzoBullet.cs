using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanzoBullet : MonoBehaviour
{
    public GameObject bullet3_2;
    public int damage = 10;
    public float posX = 4.9f, posY = 0f;

    // Update is called once per frame
    void Update()
    {

    }

    void Bullet3_2()
    {
        //Vector2 pos = new Vector2(posX, posY);
        Vector2 pos = new Vector2(transform.position.x + posX, transform.position.y + posY);
        Instantiate(bullet3_2, pos, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}

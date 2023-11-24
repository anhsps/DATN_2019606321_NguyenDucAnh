using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtk1 : MonoBehaviour
{
    //public int atkDamage = 10;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    public int damageAmount = 10;

    /*private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Kiểm tra xem có va chạm giữa SpriteRenderer của Enemy và SpriteRenderer của Player không
            if (IsSpriteRendererCollision(gameObject.GetComponent<SpriteRenderer>(), player.GetComponent<SpriteRenderer>()))
            {
                // Gọi phương thức xử lý sát thương cho Player
                player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            }
        }
    }*/

    // Hàm kiểm tra xem có va chạm giữa hai SpriteRenderer không
    private bool IsSpriteRendererCollision(SpriteRenderer spriteRenderer1, SpriteRenderer spriteRenderer2)
    {
        if (spriteRenderer1 == null || spriteRenderer2 == null)
        {
            // Nếu một trong hai là null, không thể kiểm tra va chạm
            return false;
        }

        // Lấy các biên giới của SpriteRenderer
        Bounds bounds1 = spriteRenderer1.bounds;
        Bounds bounds2 = spriteRenderer2.bounds;

        // Kiểm tra xem hai biên giới có giao nhau hay không
        return bounds1.Intersects(bounds2);
    }

    void Attack1()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Kiểm tra xem có va chạm giữa SpriteRenderer của Enemy và SpriteRenderer của Player không
            if (IsSpriteRendererCollision(gameObject.GetComponent<SpriteRenderer>(), player.GetComponent<SpriteRenderer>()))
            {
                // Gọi phương thức xử lý sát thương cho Player
                player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            }
        }
    }
}

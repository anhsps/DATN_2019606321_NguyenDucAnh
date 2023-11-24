using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//dính vị trí player vs moving platform
public class StickyPlatform : MonoBehaviour
{
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {//dính vị trí player vs moving platform
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {//thoát dính
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}

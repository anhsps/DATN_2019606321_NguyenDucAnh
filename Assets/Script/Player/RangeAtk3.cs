using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAtk3 : MonoBehaviour
{
    PlayerCombat pcb;
    PlayerController pc;
    [HideInInspector] public Vector2 newPos3_1, newPos3_2, newPos3_3;

    // Start is called before the first frame update
    void Start()
    {
        pcb = GetComponentInParent<PlayerCombat>();
        pc = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (pc.faceingRight)
            {
                newPos3_1 = new Vector2(transform.position.x + (newPos3_3.x - transform.position.x) / 3f, pcb.transform.position.y);
                newPos3_2 = new Vector2(transform.position.x + (newPos3_3.x - transform.position.x) * 2 / 3f, pcb.transform.position.y);
                newPos3_3 = new Vector2(collision.transform.position.x - 1f, pcb.transform.position.y);
            }
            else
            {
                newPos3_1 = new Vector2(transform.position.x - (transform.position.x - newPos3_3.x) / 3f, pcb.transform.position.y);
                newPos3_2 = new Vector2(transform.position.x - (transform.position.x - newPos3_3.x) * 2 / 3f, pcb.transform.position.y);
                newPos3_3 = new Vector2(collision.transform.position.x + 1f, pcb.transform.position.y);
            }
            pcb.rangeAtk3 = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            pcb.rangeAtk3 = false;
        }
    }
}

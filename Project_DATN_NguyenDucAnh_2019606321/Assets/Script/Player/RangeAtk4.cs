using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAtk4 : MonoBehaviour
{
    PlayerCombat pcb;
    [HideInInspector] public Vector2 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        pcb = GetComponentInParent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            newPosition = new Vector2(collision.transform.position.x, pcb.gunTip4.position.y);
            pcb.rangeAtk4 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            pcb.rangeAtk4 = false;
        }
    }
}

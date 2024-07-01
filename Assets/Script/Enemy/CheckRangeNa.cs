using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//phạm vi atk của boss Naruto
public class CheckRangeNa : MonoBehaviour
{
    BossNaruto bParent;

    // Start is called before the first frame update
    void Start()
    {
        bParent = GetComponentInParent<BossNaruto>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bParent.inRange = true;
            bParent.target = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bParent.inRange = false;
            bParent.SelectTarget();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPCollect : MonoBehaviour
{
    public int mp = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerHealth>().AddMP(mp);
        }
    }
}

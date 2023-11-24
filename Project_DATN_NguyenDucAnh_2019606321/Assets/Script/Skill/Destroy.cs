using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float t = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, t);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

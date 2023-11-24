using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//gây đam cho player khi atk
public class EnemyWeapon : MonoBehaviour
{
    public int atkDamage = 10;
    public Vector3 atkOffset;
    public float atkRange;
    public LayerMask atkLayers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack1()
    {
        Vector3 pos = transform.position;
        pos += transform.right * atkOffset.x;
        pos += transform.up * atkOffset.y;

        Collider2D col = Physics2D.OverlapCircle(pos, atkRange, atkLayers);
        if (col != null)
        {
            col.GetComponent<PlayerHealth>().TakeDamage(atkDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * atkOffset.x;
        pos += transform.up * atkOffset.y;
        
        Gizmos.color = Color.red;//có thể bỏ
        Gizmos.DrawWireSphere(pos, atkRange);       
    }
}

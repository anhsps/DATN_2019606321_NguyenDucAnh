using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//gây đam cho player khi atk gần
public class EnemyWeapon : MonoBehaviour
{
    public int atkDamage = 10;
    public Vector3 atkPoint;
    public float atkRange;

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
        pos += transform.right * atkPoint.x;
        pos += transform.up * atkPoint.y;

        Collider2D col = Physics2D.OverlapCircle(pos, atkRange, LayerMask.GetMask("Player"));
        if (col != null)
            col.GetComponent<PlayerHealth>().TakeDamage(atkDamage);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * atkPoint.x;
        pos += transform.up * atkPoint.y;

        Gizmos.DrawWireSphere(pos, atkRange);
        Gizmos.color = Color.red;//có thể bỏ
    }
}

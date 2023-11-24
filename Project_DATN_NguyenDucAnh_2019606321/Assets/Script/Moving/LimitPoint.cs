using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//di chuyển platform các điểm
public class LimitPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] limitPoints;
    private int currentLimitPointIndex = 0;//chỉ số limitpoint hiện tại
    [SerializeField] private float speed = 2f;
    public bool checkFlip = true;

    SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(limitPoints[currentLimitPointIndex].transform.position, transform.position) < .1f)
        {
            currentLimitPointIndex++;
            if (currentLimitPointIndex >= limitPoints.Length)
            {
                currentLimitPointIndex = 0;
            }
        }
        //d/c tới vị trí điểm
        transform.position = Vector2.MoveTowards(transform.position, 
            limitPoints[currentLimitPointIndex].transform.position, Time.deltaTime * speed);

        //quay mặt
        if (checkFlip)
        {
            if (currentLimitPointIndex % 2 == 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
    }
}

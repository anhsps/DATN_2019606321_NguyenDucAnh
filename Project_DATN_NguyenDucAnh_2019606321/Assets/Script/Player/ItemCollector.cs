using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//coin
public class ItemCollector : MonoBehaviour
{
    //public int bullets = 10;//số đạn
    [HideInInspector] public int items = 0;//item nhặt
    public int maxItem = 30;
    //public LayerMask itemLayer;
    public BoxCollider2D boxCollider;
    public AudioSource collectItemAudio;

    [SerializeField] private Text ItemScore;
    [SerializeField] private Text textWin;
    [SerializeField] private Text tempItemScore;

    void Start()
    {
        ItemScore.text = "" + items;
    }

    void Update()
    {
        //Physics2D.OverlapBoxAll: check va chạm với layer "Item" trong một khu vực hình hộp xung quanh Player
        //0f: góc quay của hình hộp kiểm tra va chạm
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0f, LayerMask.GetMask("Item"));
        foreach (Collider2D collider in colliders)
        {
            Destroy(collider.gameObject);
            items++;
            ItemScore.text = "" + items;

            //hiển thị trong menu game win
            textWin.text = items.ToString() + " / " + maxItem;

            //lưu số item tạm để sử dụng tính toán
            tempItemScore.text = items.ToString();

            collectItemAudio.Play();
        }
    }
}

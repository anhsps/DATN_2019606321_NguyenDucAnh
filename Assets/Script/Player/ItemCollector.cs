using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//coin
public class ItemCollector : MonoBehaviour
{
    [HideInInspector] public int items = 0;//item nhặt
    [HideInInspector] public int maxItem = 30;
    public AudioSource collectItemAudio;
    public Text tempItemScore;

    [SerializeField] private Text ItemScore;
    [SerializeField] private Text textWin;
    BoxCollider2D box;

    void Start()
    {
        ItemScore.text = "" + items;
        box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //Physics2D.OverlapBoxAll: check va chạm với layer "Item" trong một khu vực hình hộp xung quanh Player
        //0f: góc quay của hình hộp kiểm tra va chạm
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, box.size, 0f, LayerMask.GetMask("Item"));
        foreach (Collider2D collider in colliders)
        {
            collectItemAudio.Play();
            Destroy(collider.gameObject);
            items++;
            ItemScore.text = "" + items;

            textWin.text = items.ToString() + " / " + maxItem;//hiển thị trong menu game win
            tempItemScore.text = items.ToString();//lưu số item tạm để sử dụng tính toán
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerLife : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public AudioSource WinAudio;
    ItemCollector ic;
    Rigidbody2D rb;
    Animator animator;

    private static readonly string Level1Point = "level1point";
    private static readonly string Level2Point = "level2point";
    private static readonly string Level3Point = "level3point";
    private static readonly string Level4Point = "level4point";
    private static readonly string Level5Point = "level5point";

    //menu gamewin
    [SerializeField] private GameObject GameWinUI;
    [SerializeField] private GameObject buttonPause;

    [SerializeField] private Text tempNumberItems;
    [SerializeField] private GameObject zeroStar;
    [SerializeField] private GameObject oneStar;
    [SerializeField] private GameObject twoStar;
    [SerializeField] private GameObject threeStar;

    private void Start()
    {
        ic = GetComponent<ItemCollector>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0f, LayerMask.GetMask("Finish"));
        foreach (Collider2D collider in colliders)
        {
            rb.bodyType = RigidbodyType2D.Static;
            GetComponent<PlayerController>().enabled = false;
            GetComponent<PlayerCombat>().enabled = false;
            animator.SetBool("Run", false);
            animator.SetBool("Fall", false);
            Invoke("Win", 1f);//gọi hàm tên Win sau ít s          
            buttonPause.SetActive(false);
            WinAudio.Play();
            Destroy(collider.gameObject, 0.5f);

            //lưu điểm cao nhất
            checkMaxPointMap();

            //hiển thị số sao đạt được
            starScoreUI();
        }
    }

    void Win()
    {
        GameWinUI.SetActive(true);
    }

    //kiểm tra điểm cao nhất truyền về menulevel
    private void checkMaxPointMap()
    {
        if (SceneManager.GetActiveScene().name.CompareTo("Map1") == 0)
        {
            int temp = iteamScore(PlayerPrefs.GetInt(Level1Point));
            PlayerPrefs.SetInt(Level1Point, temp);                    // Cập nhật lại điểm cao nhất
        }
        else if (SceneManager.GetActiveScene().name.CompareTo("Map2") == 0)
        {
            int temp = iteamScore(PlayerPrefs.GetInt(Level2Point));
            PlayerPrefs.SetInt(Level2Point, temp);                  // Cập nhật lại điểm cao nhất
        }
        else if (SceneManager.GetActiveScene().name.CompareTo("Map3") == 0)
        {
            int temp = iteamScore(PlayerPrefs.GetInt(Level3Point));
            PlayerPrefs.SetInt(Level3Point, temp);                   // Cập nhật lại điểm cao nhất
        }
        else if (SceneManager.GetActiveScene().name.CompareTo("Map4") == 0)
        {
            int temp = iteamScore(PlayerPrefs.GetInt(Level4Point));
            PlayerPrefs.SetInt(Level4Point, temp);                   // Cập nhật lại điểm cao nhất
        }
        else if (SceneManager.GetActiveScene().name.CompareTo("Map5") == 0)
        {
            int temp = iteamScore(PlayerPrefs.GetInt(Level5Point));
            PlayerPrefs.SetInt(Level5Point, temp);                   // Cập nhật lại điểm cao nhất
        }
    }

    //hien thi so sao khi win
    private void starScoreUI()
    {
        int items = int.Parse(tempNumberItems.text);
        if (items < ic.maxItem/3)
        {
            zeroStar.SetActive(true);
        }
        else if (items >= ic.maxItem / 3 && items < ic.maxItem / 3 * 2)
        {
            oneStar.SetActive(true);
        }
        else if (items >= ic.maxItem / 3 * 2 && items < ic.maxItem)
        {
            twoStar.SetActive(true);
        }
        else if (items == ic.maxItem)
        {
            threeStar.SetActive(true);
        }
    }

    //so sanh ket qua cũ
    private int iteamScore(int maxPointOld)
    {
        int items = int.Parse(tempNumberItems.text);
        if (items > maxPointOld)
        {
            return items;
        }
        else
        {
            return maxPointOld;
        }
    }
}

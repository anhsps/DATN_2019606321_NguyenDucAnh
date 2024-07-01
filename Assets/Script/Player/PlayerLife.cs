using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public AudioSource WinAudio;
    BoxCollider2D box;
    Rigidbody2D rb;
    Animator animator;
    ItemCollector ic;

    private static readonly Dictionary<string, string> LevelPoints = new Dictionary<string, string>
    {
        {"Map1","level1point" },
        {"Map2","level2point" },
        {"Map3","level3point" },
        {"Map4","level4point" },
        {"Map5","level5point" },
        {"Map6","level6point" },
        {"Map7","level7point" },
        {"Map8","level8point" },
        {"Map9","level9point" },
        {"Map10","level10point" }
    };

    //menu gamewin
    [SerializeField] private GameObject GameWinUI, buttonUI;
    [SerializeField] private GameObject zeroStar, oneStar, twoStar, threeStar;

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ic = GetComponent<ItemCollector>();
    }

    private void Update()
    {
        Collider2D col = Physics2D.OverlapBox(transform.position, box.size, 0f, LayerMask.GetMask("Finish"));
        if (col != null)
        {
            Destroy(col.gameObject, 0.5f);
            Invoke("Win", 1f);//gọi hàm tên Win sau ít s          
            rb.bodyType = RigidbodyType2D.Static;
            GetComponent<PlayerController>().enabled = false;
            GetComponent<PlayerCombat>().enabled = false;
            animator.SetBool("Run", false);
            buttonUI.SetActive(false);
            WinAudio.Play();

            UpdateMaxPoints();//lưu điểm cao nhất truyền về menulevel
            DisplayStarScore();//hiển thị số sao đạt được
        }
    }

    private void Win() { GameWinUI.SetActive(true); }

    private void UpdateMaxPoints()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        //kiểm tra xem từ điển có chứa tên cảnh hiện tại ko
        if (LevelPoints.TryGetValue(sceneName, out string levelPoint))
        {
            int currentPoints = int.Parse(ic.tempItemScore.text);
            int maxPointOld = PlayerPrefs.GetInt(levelPoint);
            int maxPoints = Mathf.Max(currentPoints, maxPointOld);
            //c2: int maxPoints = (currentPoints > maxPointOld) ? currentPoints : maxPointOld;
            PlayerPrefs.SetInt(levelPoint, maxPoints);//update điểm cao nhất của level
        }
    }

    private void DisplayStarScore()
    {
        int items = int.Parse(ic.tempItemScore.text);
        if (items < ic.maxItem / 3)
            zeroStar.SetActive(true);//zeroStar.SetActive(items<ic.maxItem/3);
        else if (items < ic.maxItem / 3 * 2)
            oneStar.SetActive(true);
        else if (items < ic.maxItem)
            twoStar.SetActive(true);
        else
            threeStar.SetActive(true);
    }
}

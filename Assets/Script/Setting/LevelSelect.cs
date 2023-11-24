using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public int maxItem = 30;
    
    private static readonly string FirstPlay = "FirstPlay";

    private static readonly string Level1Point = "level1point";
    private static readonly string Level2Point = "level2point";
    private static readonly string Level3Point = "level3point";
    private static readonly string Level4Point = "level4point";//
    private static readonly string Level5Point = "level5point";

    #region biến lv
    [SerializeField] private GameObject LV2;
    [SerializeField] private GameObject LV3;
    [SerializeField] private GameObject LV4;
    [SerializeField] private GameObject LV5;

    [SerializeField] private GameObject level1_s0;
    [SerializeField] private GameObject level1_s1;
    [SerializeField] private GameObject level1_s2;
    [SerializeField] private GameObject level1_s3;

    [SerializeField] private GameObject level2_lock;
    [SerializeField] private GameObject level2_s0;
    [SerializeField] private GameObject level2_s1;
    [SerializeField] private GameObject level2_s2;
    [SerializeField] private GameObject level2_s3;

    [SerializeField] private GameObject level3_lock;
    [SerializeField] private GameObject level3_s0;
    [SerializeField] private GameObject level3_s1;
    [SerializeField] private GameObject level3_s2;
    [SerializeField] private GameObject level3_s3;

    [SerializeField] private GameObject level4_lock;
    [SerializeField] private GameObject level4_s0;
    [SerializeField] private GameObject level4_s1;
    [SerializeField] private GameObject level4_s2;
    [SerializeField] private GameObject level4_s3;

    [SerializeField] private GameObject level5_lock;
    [SerializeField] private GameObject level5_s0;
    [SerializeField] private GameObject level5_s1;
    [SerializeField] private GameObject level5_s2;
    [SerializeField] private GameObject level5_s3;
    #endregion biến lv

    //luu diem max cua moi level
    private int level1Score;
    private int level2Score;
    private int level3Score, level4Score, level5Score;

    private int firstPlayInt;

    private void Start()
    {
        
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)
        {
            PlayerPrefs.SetInt(Level1Point, 0);
            PlayerPrefs.SetInt(Level2Point, 0);
            PlayerPrefs.SetInt(Level3Point, 0);
            PlayerPrefs.SetInt(Level4Point, 0);
            PlayerPrefs.SetInt(Level5Point, 0);

            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            level1Score = PlayerPrefs.GetInt(Level1Point);
            level2Score = PlayerPrefs.GetInt(Level2Point);
            level3Score = PlayerPrefs.GetInt(Level3Point);
            level4Score = PlayerPrefs.GetInt(Level4Point);
            level5Score = PlayerPrefs.GetInt(Level5Point);
        }

        checkStars(maxItem, level1Score, null, level1_s0, level1_s1, level1_s2, level1_s3);
        checkStars(PlayerPrefs.GetInt(Level1Point), level2Score, level2_lock, level2_s0, level2_s1, level2_s2, level2_s3);
        checkStars(PlayerPrefs.GetInt(Level2Point), level3Score, level3_lock, level3_s0, level3_s1, level3_s2, level3_s3);
        checkStars(PlayerPrefs.GetInt(Level3Point), level4Score, level4_lock, level4_s0, level4_s1, level4_s2, level4_s3);
        checkStars(PlayerPrefs.GetInt(Level4Point), level5Score, level5_lock, level5_s0, level5_s1, level5_s2, level5_s3);

    }

    private void checkStars(int diemtruoc, int diem, GameObject lockmap, GameObject khongsao, GameObject motsao, GameObject haisao, GameObject basao)
    {
        if (diemtruoc > 0)
        {
            if (diem < maxItem/3)
            {
                khongsao.SetActive(true);
            }
            else if (diem >= maxItem/3 && diem < maxItem / 3 * 2)
            {
                motsao.SetActive(true);
            }
            else if (diem >= maxItem / 3 * 2 && diem < maxItem)
            {
                haisao.SetActive(true);
            }
            else if (diem == maxItem)
            {
                basao.SetActive(true);
            }
            else
            {
                khongsao.SetActive(true);
            }
        }
        else
        {
            lockmap.SetActive(true);
        }
    }

    public void Level1Load()
    {
        SceneManager.LoadScene("Map1");
    }
    public void Level2Load()
    {
        SceneManager.LoadScene("Map2");
    }
    public void Level3Load()
    {
        SceneManager.LoadScene("Map3");
    }
    public void Level4Load()
    {
        SceneManager.LoadScene("Map4");
    }
    public void Level5Load()
    {
        SceneManager.LoadScene("Map5");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

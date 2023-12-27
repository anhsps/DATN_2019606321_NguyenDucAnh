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
    private static readonly string Level4Point = "level4point";
    private static readonly string Level5Point = "level5point";
    private static readonly string Level6Point = "level6point";//
    private static readonly string Level7Point = "level7point";
    private static readonly string Level8Point = "level8point";
    private static readonly string Level9Point = "level9point";
    private static readonly string Level10Point = "level10point";

    #region biến lv
    [SerializeField] private GameObject LV2, LV3, LV4, LV5, LV6, LV7, LV8, LV9, LV10;

    [SerializeField] private GameObject level1_s0, level1_s1, level1_s2, level1_s3;

    [SerializeField] private GameObject level2_lock, level2_s0, level2_s1, level2_s2, level2_s3;
    [SerializeField] private GameObject level3_lock, level3_s0, level3_s1, level3_s2, level3_s3;
    [SerializeField] private GameObject level4_lock, level4_s0, level4_s1, level4_s2, level4_s3;
    [SerializeField] private GameObject level5_lock, level5_s0, level5_s1, level5_s2, level5_s3;
    [SerializeField] private GameObject level6_lock, level6_s0, level6_s1, level6_s2, level6_s3;//
    [SerializeField] private GameObject level7_lock, level7_s0, level7_s1, level7_s2, level7_s3;
    [SerializeField] private GameObject level8_lock, level8_s0, level8_s1, level8_s2, level8_s3;
    [SerializeField] private GameObject level9_lock, level9_s0, level9_s1, level9_s2, level9_s3;
    [SerializeField] private GameObject level10_lock, level10_s0, level10_s1, level10_s2, level10_s3;
    #endregion biến lv

    //luu diem max cua moi level
    private int level1Score, level2Score, level3Score, level4Score, level5Score,
        level6Score, level7Score, level8Score, level9Score, level10Score;

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
            PlayerPrefs.SetInt(Level6Point, 0);//
            PlayerPrefs.SetInt(Level7Point, 0);
            PlayerPrefs.SetInt(Level8Point, 0);
            PlayerPrefs.SetInt(Level9Point, 0);
            PlayerPrefs.SetInt(Level10Point, 0);

            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            level1Score = PlayerPrefs.GetInt(Level1Point);
            level2Score = PlayerPrefs.GetInt(Level2Point);
            level3Score = PlayerPrefs.GetInt(Level3Point);
            level4Score = PlayerPrefs.GetInt(Level4Point);
            level5Score = PlayerPrefs.GetInt(Level5Point);
            level6Score = PlayerPrefs.GetInt(Level6Point);//
            level7Score = PlayerPrefs.GetInt(Level7Point);
            level8Score = PlayerPrefs.GetInt(Level8Point);
            level9Score = PlayerPrefs.GetInt(Level9Point);
            level10Score = PlayerPrefs.GetInt(Level10Point);
        }

        checkStars(maxItem, level1Score, null, level1_s0, level1_s1, level1_s2, level1_s3);
        checkStars(PlayerPrefs.GetInt(Level1Point), level2Score, level2_lock, level2_s0, level2_s1, level2_s2, level2_s3);
        checkStars(PlayerPrefs.GetInt(Level2Point), level3Score, level3_lock, level3_s0, level3_s1, level3_s2, level3_s3);
        checkStars(PlayerPrefs.GetInt(Level3Point), level4Score, level4_lock, level4_s0, level4_s1, level4_s2, level4_s3);
        checkStars(PlayerPrefs.GetInt(Level4Point), level5Score, level5_lock, level5_s0, level5_s1, level5_s2, level5_s3);
        checkStars(PlayerPrefs.GetInt(Level5Point), level6Score, level6_lock, level6_s0, level6_s1, level6_s2, level6_s3);//
        checkStars(PlayerPrefs.GetInt(Level6Point), level7Score, level7_lock, level7_s0, level7_s1, level7_s2, level7_s3);
        checkStars(PlayerPrefs.GetInt(Level7Point), level8Score, level8_lock, level8_s0, level8_s1, level8_s2, level8_s3);
        checkStars(PlayerPrefs.GetInt(Level8Point), level9Score, level9_lock, level9_s0, level9_s1, level9_s2, level9_s3);
        checkStars(PlayerPrefs.GetInt(Level9Point), level10Score, level10_lock, level10_s0, level10_s1, level10_s2, level10_s3);
    }

    private void checkStars(int diemtruoc, int diem, GameObject lockmap, GameObject khongsao, GameObject motsao, GameObject haisao, GameObject basao)
    {
        if (diemtruoc > 0)
        {
            if (diem < maxItem / 3)
            {
                khongsao.SetActive(true);
            }
            else if (diem >= maxItem / 3 && diem < maxItem / 3 * 2)
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
    public void Level6Load()
    {
        SceneManager.LoadScene("Map6");
    }
    public void Level7Load()
    {
        SceneManager.LoadScene("Map7");
    }
    public void Level8Load()
    {
        SceneManager.LoadScene("Map8");
    }
    public void Level9Load()
    {
        SceneManager.LoadScene("Map9");
    }
    public void Level10Load()
    {
        SceneManager.LoadScene("Map10");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private int maxItem = 30;

    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string[] LevelPoints = {
        "level1point", "level2point", "level3point", "level4point", "level5point",
        "level6point", "level7point", "level8point", "level9point", "level10point" };

    [SerializeField] private GameObject[] level_lock = new GameObject[10];
    [SerializeField] private GameObject[] level_s0 = new GameObject[10];
    [SerializeField] private GameObject[] level_s1 = new GameObject[10];
    [SerializeField] private GameObject[] level_s2 = new GameObject[10];
    [SerializeField] private GameObject[] level_s3 = new GameObject[10];

    //luu diem max cua moi level
    private int[] LevelScores = new int[10];

    private void Start()
    {
        if (PlayerPrefs.GetInt(FirstPlay) == 0)
        {
            PlayerPrefs.SetInt(FirstPlay, -1);
            foreach (string levelPoint in LevelPoints)
            {
                PlayerPrefs.SetInt(levelPoint, 0);
            }
        }
        else
        {
            for (int i = 0; i < LevelPoints.Length; i++)
            {
                LevelScores[i] = PlayerPrefs.GetInt(LevelPoints[i]);
            }
        }

        for (int i = 0; i < LevelPoints.Length; i++)
        {
            if (i == 0)
                CheckStars(maxItem, LevelScores[i], null, level_s0[i], level_s1[i], level_s2[i], level_s3[i]);
            else
                CheckStars(PlayerPrefs.GetInt(LevelPoints[i - 1]), LevelScores[i],
                    level_lock[i], level_s0[i], level_s1[i], level_s2[i], level_s3[i]);
        }
    }

    private void CheckStars(int diemLVtruoc, int diem, GameObject lockmap, GameObject zeroStar, GameObject oneStar, GameObject twoStar, GameObject threeStar)
    {
        if (diemLVtruoc > 0)
        {
            if (diem < maxItem / 3)
                zeroStar.SetActive(true);
            else if (diem < maxItem / 3 * 2)
                oneStar.SetActive(true);
            else if (diem < maxItem)
                twoStar.SetActive(true);
            else
                threeStar.SetActive(true);
        }
        else
            lockmap.SetActive(true);
    }

    public void Level1Load() { SceneManager.LoadScene("Map1"); }
    public void Level2Load() { SceneManager.LoadScene("Map2"); }
    public void Level3Load() { SceneManager.LoadScene("Map3"); }
    public void Level4Load() { SceneManager.LoadScene("Map4"); }
    public void Level5Load() { SceneManager.LoadScene("Map5"); }
    public void Level6Load() { SceneManager.LoadScene("Map6"); }
    public void Level7Load() { SceneManager.LoadScene("Map7"); }
    public void Level8Load() { SceneManager.LoadScene("Map8"); }
    public void Level9Load() { SceneManager.LoadScene("Map9"); }
    public void Level10Load() { SceneManager.LoadScene("Map10"); }
    public void LoadMenu() { SceneManager.LoadScene("Menu"); }
}

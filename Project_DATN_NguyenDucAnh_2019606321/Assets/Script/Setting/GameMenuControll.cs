using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuControll : MonoBehaviour
{
    public static bool GameIsPaused = false;

    private void Start()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Out game");
        Application.Quit();
    }

    public void ReplayGame()
    {//chơi lại màn chơi       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

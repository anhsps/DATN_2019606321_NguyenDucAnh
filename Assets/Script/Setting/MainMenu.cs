using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider volumSlider, effectSlider;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game!");
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Clear all");

        volumSlider.value = effectSlider.value = 0.5f;
    }
}


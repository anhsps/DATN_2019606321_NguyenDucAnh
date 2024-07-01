using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider, effectSlider;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game!");
    }

    public void StartGame()
    {//sau khi ResetGame
        SceneManager.LoadScene("Start");
        Debug.Log("Start game!");
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Clear all");

        //volumSlider.value = effectSlider.value = 0.5f;
        SetSliderValues(0.5f);
    }
    void SetSliderValues(float value)
    {
        volumeSlider.value = effectSlider.value = value;

        // (k bắt buộc) Kích hoạt sự kiện onValueChanged để đảm bảo rằng các xử lý liên quan đến sự kiện được thực hiện
        volumeSlider.onValueChanged.Invoke(value);
        effectSlider.onValueChanged.Invoke(value);
    }
}

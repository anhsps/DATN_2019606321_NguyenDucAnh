using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    private static readonly string FirstSound = "FirstSound";
    private static readonly string soundBackgroundPref = "soundBackgroundPref";
    private static readonly string soundEffectsPref = "soundEffectsPref";

    public Slider soundBGSlider, soundEffectsSlider;
    public AudioSource soundBGAudio;
    public AudioSource[] soundEffectsAudio;

    private void Awake()
    {
        float soundBGFloat = PlayerPrefs.GetFloat(soundBackgroundPref);
        float soundEFloat = PlayerPrefs.GetFloat(soundEffectsPref);

        if (PlayerPrefs.GetInt(FirstSound) == 0 ||
            (PlayerPrefs.GetInt(FirstSound) != 0 && soundBGFloat == 0 && soundEFloat == 0))
        {
            PlayerPrefs.SetInt(FirstSound, -1);
            //Lấy gt từ PlayerPrefs or sd gt mặc định 0.5f nếu k có
            soundBGSlider.value = PlayerPrefs.GetFloat(soundBackgroundPref, 0.5f);
            soundEffectsSlider.value = PlayerPrefs.GetFloat(soundEffectsPref, 0.5f);
        }
        else
        {
            soundBGSlider.value = soundBGFloat;
            soundEffectsSlider.value = soundEFloat;
        }
    }

    public void SaveSoundSetting()
    {
        PlayerPrefs.SetFloat(soundBackgroundPref, soundBGSlider.value);
        PlayerPrefs.SetFloat(soundEffectsPref, soundEffectsSlider.value);
        PlayerPrefs.Save(); // Đảm bảo lưu thay đổi
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            SaveSoundSetting();
    }

    public void UpdateSound()
    {
        soundBGAudio.volume = soundBGSlider.value;
        foreach (AudioSource audioSource in soundEffectsAudio)
        {
            audioSource.volume = soundEffectsSlider.value;
        }
        #region //c2:
        /*for (int i = 0; i < soundEffectsAudio.Length; i++)
        {
            soundEffectsAudio[i].volume = soundEffectsSlider.value;
        }*/
        #endregion
    }
}

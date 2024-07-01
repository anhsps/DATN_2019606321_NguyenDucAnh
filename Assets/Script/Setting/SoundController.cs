using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    private static readonly string soundBackgroundPref = "soundBackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";

    public Slider soundBGSlider, soundEffectsSlider;
    public AudioSource soundBGAudio;
    public AudioSource[] soundEffectsAudio;

    private void Awake()
    {
        soundBGSlider.value = PlayerPrefs.GetFloat(soundBackgroundPref, 0.5f);// Lấy gt từ PlayerPrefs or sd gt mặc định 0.5f nếu k có
        soundEffectsSlider.value = PlayerPrefs.GetFloat(SoundEffectsPref, 0.5f);
    }

    public void SaveSoundSetting()
    {
        PlayerPrefs.SetFloat(soundBackgroundPref, soundBGSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);
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

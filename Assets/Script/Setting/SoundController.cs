using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{

    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";

    public Slider backgroundSlider, soundEffectsSlider;
    private float backgroundFloat, soundEffectsFloat;
    //thêm file âm thanh 
    public AudioSource backgroundAudio;
    public AudioSource[] soundEffectsAudio;

    private void Start()
    {
        backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref, 0.5f);// Lấy gt từ PlayerPrefs or sd gt mặc định 0.5f nếu k có
        backgroundSlider.value = backgroundFloat;
        soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref, 0.5f);
        soundEffectsSlider.value = soundEffectsFloat;
    }

    public void SaveSoundSetting()
    {
        PlayerPrefs.SetFloat(BackgroundPref, backgroundSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);
        PlayerPrefs.Save(); // Đảm bảo lưu thay đổi
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveSoundSetting();
        }
    }

    public void UpdateSound()
    {
        backgroundAudio.volume = backgroundSlider.value;
        for (int i = 0; i < soundEffectsAudio.Length; i++)
        {
            soundEffectsAudio[i].volume = soundEffectsSlider.value;
        }
    }

}

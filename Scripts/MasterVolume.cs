using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolume : MonoBehaviour
{
    public Slider VolumeSlider;
    float masterVolume;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
            VolumeSlider.value = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            masterVolume = VolumeSlider.value;
            AdjustVolume(masterVolume);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            VolumeSlider.value = PlayerPrefs.GetFloat("volume");
        }
    }

    public void AdjustVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("volume", newVolume);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }

    public void DecreaseVolume()
    {
        if (PlayerPrefs.GetFloat("volume") <= 1.0f && PlayerPrefs.GetFloat("volume") >= 0.0f)
        {
            AdjustVolume(PlayerPrefs.GetFloat("volume") - 0.1f);
        }
    }

    public void IncreaseVolume()
    {
        if(PlayerPrefs.GetFloat("volume") <= 1.0f && PlayerPrefs.GetFloat("volume") >= 0.0f)
        {
            AdjustVolume(PlayerPrefs.GetFloat("volume") + 0.1f);
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManagerMenu : MonoBehaviour
{
    public static float sfxVolume;
    public static float musicVolume;

    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip musicClip;

    private void Start()
    {
        // Assign clip before playing
        musicSource.clip = musicClip;
        musicSource.loop = true; // optional
        musicSource.Play();

        // Initialize sliders if needed
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = 1f;
            musicVolumeSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = 1f;
            sfxVolumeSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });
        }

        // Apply initial volumes
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMusicVolume()
    {
        float volume = musicVolumeSlider.value;
        musicVolume = volume;

        float dbValue = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("Music", dbValue);
    }

    public void SetSFXVolume()
    {
        float volume = sfxVolumeSlider.value;
        sfxVolume = volume;

        float dbValue = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("SFX", dbValue);
    }
}
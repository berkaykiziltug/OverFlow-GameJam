using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AudioManagerMenu : MonoBehaviour
{
    public static AudioManagerMenu Instance;
    public static float sfxVolume;
    public static float musicVolume;
    [SerializeField] private PlayButton playButton;

    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private AudioMixer audioMixer;
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    
    
    [SerializeField] private AudioClip mainMenuMusicClip;
    [SerializeField] private AudioClip gameMusicClip;
    
    
    [Header("Mixer Groups")]
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        musicSource.clip = mainMenuMusicClip;
        musicSource.loop = true; 
        musicSource.Play();

        playButton.onPlayButtonPressedEventHandler += (sender,e) =>
        {
            musicSource.clip = gameMusicClip;
            musicSource.loop = true;
            musicSource.Play();
        };

        //Initialize Slider.
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = .2f;
            musicVolumeSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = 1f;
            sfxVolumeSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });
        }

        //Set the initialized volumes.
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

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public float GetMusicVolume()
    {
        return  musicVolume;
    }
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            // Make sure the source is routed to the SFX group
            if (sfxGroup != null)
                sfxSource.outputAudioMixerGroup = sfxGroup;

            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlaySFXPitch(AudioClip clip, float minPitch, float maxPitch)
    {
        if (clip != null && sfxSource != null)
        {
            
            if (sfxGroup != null)
                sfxSource.outputAudioMixerGroup = sfxGroup;

            
            sfxSource.pitch = Random.Range(minPitch, maxPitch);
        
            
            sfxSource.PlayOneShot(clip);
        
            
            sfxSource.pitch = 1f;
        }
    }

}
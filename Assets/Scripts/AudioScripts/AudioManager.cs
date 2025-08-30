using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

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
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private AudioClip letterFallClip;

    private void Start()
    {
        musicSource.clip = musicClip;
        musicSource.Play();
                                        
    }
}

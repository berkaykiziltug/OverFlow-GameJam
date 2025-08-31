using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioClip onPlayButtonClip;
    [SerializeField] private AudioClip onPlayButtonPressedClip;

    public EventHandler onPlayButtonPressedEventHandler;
    private Button playButton;

    void Start()
    {
        playButton = GetComponent<Button>();
        
        playButton.onClick.AddListener(() => 
            AudioManagerMenu.Instance.PlaySFX(onPlayButtonPressedClip));

        playButton.onClick.AddListener(PlayButtonOnClickedFireEvent);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManagerMenu.Instance.PlaySFX(onPlayButtonClip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void PlayButtonOnClickedFireEvent()
    {
        onPlayButtonPressedEventHandler?.Invoke(this, EventArgs.Empty);
    }
    
}
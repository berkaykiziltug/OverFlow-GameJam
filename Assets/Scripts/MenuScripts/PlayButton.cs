using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioClip onPlayButtonClip;
    [SerializeField] private AudioClip onPlayButtonPressedClip;

    private Button playButton;

    void Start()
    {
        playButton = GetComponent<Button>();
        
        playButton.onClick.AddListener(() => 
            AudioManagerMenu.Instance.PlaySFX(onPlayButtonPressedClip));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManagerMenu.Instance.PlaySFX(onPlayButtonClip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
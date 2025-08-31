using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioClip onSettingsButtonClip;
    [SerializeField] private AudioClip onSettingsButtonPressedClip;

    private Button playButton;

    void Start()
    {
        playButton = GetComponent<Button>();
        
        playButton.onClick.AddListener(() => 
            AudioManagerMenu.Instance.PlaySFX(onSettingsButtonPressedClip));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManagerMenu.Instance.PlaySFX(onSettingsButtonClip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
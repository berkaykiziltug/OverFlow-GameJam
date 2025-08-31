using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KerimButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioClip onSettingsButtonClip;
    [SerializeField] private AudioClip onSettingsButtonPressedClip;

    private Button kerimLinkedInButton;

    void Start()
    {
        kerimLinkedInButton = GetComponent<Button>();
        
        kerimLinkedInButton.onClick.AddListener(() => 
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

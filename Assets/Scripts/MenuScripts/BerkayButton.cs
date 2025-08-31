using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BerkayButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioClip onSettingsButtonClip;
    [SerializeField] private AudioClip onSettingsButtonPressedClip;

    private Button berkayLinkedInButton;

    void Start()
    {
        berkayLinkedInButton = GetComponent<Button>();
        
        berkayLinkedInButton.onClick.AddListener(() => 
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

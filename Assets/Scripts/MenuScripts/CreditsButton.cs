using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreditsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioClip onCreditsButtonClip;
    [SerializeField] private AudioClip onCreditsButtonPressedClip;

    private Button playButton;

    void Start()
    {
        playButton = GetComponent<Button>();
        
        playButton.onClick.AddListener(() => 
            AudioManagerMenu.Instance.PlaySFX(onCreditsButtonPressedClip));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManagerMenu.Instance.PlaySFX(onCreditsButtonClip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioClip onBackButtonClip;
    [SerializeField] private AudioClip onBackButtonPressedClip;

    private Button playButton;

    void Start()
    {
        playButton = GetComponent<Button>();
        
        playButton.onClick.AddListener(() => 
            AudioManagerMenu.Instance.PlaySFX(onBackButtonPressedClip));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManagerMenu.Instance.PlaySFX(onBackButtonClip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
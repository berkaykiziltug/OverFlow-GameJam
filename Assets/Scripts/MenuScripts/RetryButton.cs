using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioClip onRetryButtonClip;
    [SerializeField] private AudioClip onRetryButtonPressedClip;
    

    private Button retryButton;

    void Start()
    {
        retryButton = GetComponent<Button>();
        retryButton.onClick.AddListener(() => {
            AudioManagerMenu.Instance.PlaySFX(onRetryButtonPressedClip);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManagerMenu.Instance.PlaySFX(onRetryButtonClip);
    }
}

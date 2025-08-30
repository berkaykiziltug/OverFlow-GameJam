using System;
using EasyTextEffects;
using TMPro;
using UnityEngine;

public class CompletedWordDisplay : MonoBehaviour
{
    [SerializeField] private Typer typer;
    
    [Header("Correct Text Effect")]
    [SerializeField] private TextEffect correctTextEffect;
    [SerializeField] private TextMeshProUGUI correctWordDisplayTMP;
    [SerializeField] private GameObject correctWordDisplayGO;

    private void Awake()
    {
        
    }

    void Start()
    {
        typer.OnCorrectWordCompleted += Typer_OnWordCompletedSuccess;
        
        // Get components if not assigned
        if (correctTextEffect == null)
            correctTextEffect = GetComponent<TextEffect>();
        if (correctWordDisplayTMP == null)
            correctWordDisplayTMP = GetComponent<TextMeshProUGUI>();
    }

    private void Typer_OnWordCompletedSuccess(object sender, Typer.OnCorrectWordCompletedEventArgs e)
    {
        Debug.Log("Displaying completed word: " + e.correctWord);
        
        correctWordDisplayGO.SetActive(true);
        // Show the completed word
        correctWordDisplayTMP.text = e.correctWord;
        
        
        // Play the text effect
        if (correctTextEffect != null)
        {
            correctTextEffect.StopManualEffects();
            correctTextEffect.Refresh();
            correctTextEffect.StartManualEffect("correctComposite");
        }
    }
    
    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (typer != null)
            typer.OnCorrectWordCompleted -= Typer_OnWordCompletedSuccess;
    }
}
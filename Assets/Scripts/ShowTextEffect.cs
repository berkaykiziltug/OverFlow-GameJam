using System;
using EasyTextEffects;
using TMPro;
using UnityEngine;

public class ShowTextEffect : MonoBehaviour
{
    [SerializeField] private Typer typer;

    private TextMeshProUGUI wordDisplay;
    [SerializeField] private TextEffect textEffect;
    
    [Header("Correct Text Effect")]
    [SerializeField] private GameObject correctTextEffectGO;
    [SerializeField] private TextEffect correctTextEffect;
    [SerializeField] private TextMeshProUGUI correctWordDisplayTMP;
    

    [SerializeField] private GameObject outputTextGO;

    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        typer.OnNewWordSpawned += Typer_OnNewWordSpawned;
        typer.OnCorrectWordCompleted += Typer_OnWordCompletedSuccess;
        wordDisplay = GetComponent<TextMeshProUGUI>();
    }

    private void Typer_OnWordCompletedSuccess(object sender, Typer.OnCorrectWordCompletedEventArgs e)
    {
        Debug.Log("Received completed word: " + e.correctWord); 
        correctTextEffectGO.SetActive(true);
        correctWordDisplayTMP.text = e.correctWord;
        if (correctTextEffect != null)
        {
            correctTextEffect.StopManualEffects();
            correctTextEffect.Refresh();
            correctTextEffect.StartManualEffect("correctMove");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Typer_OnNewWordSpawned(object sender, Typer.OnNewWordSpawnedEventArgs e)
    {
        outputTextGO.SetActive(false);
        textEffect.gameObject.SetActive(true);
        wordDisplay.text = e.word;
       
        if (textEffect != null)
        {
            textEffect.StopManualEffects();
            textEffect.Refresh();
            textEffect.StartManualEffects();
        }
    }
    
    
}

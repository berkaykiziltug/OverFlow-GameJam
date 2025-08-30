using System;
using EasyTextEffects;
using TMPro;
using UnityEngine;

public class ShowTextEffectUpdated : MonoBehaviour
{
    [SerializeField] private Typer typer;
    [SerializeField] private TextEffect textEffect;
    [SerializeField] private GameObject outputTextGO;
    
    [SerializeField]private TextMeshProUGUI wordDisplay;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        typer.OnNewWordSpawned += Typer_OnNewWordSpawned;
                
    }

    void Start()
    {
        
    }

    private void Typer_OnNewWordSpawned(object sender, Typer.OnNewWordSpawnedEventArgs e)
    {
        Debug.Log("Displaying new word: " + e.word);
        
        if (outputTextGO != null)
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
    
    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (typer != null)
            typer.OnNewWordSpawned -= Typer_OnNewWordSpawned;
    }
}
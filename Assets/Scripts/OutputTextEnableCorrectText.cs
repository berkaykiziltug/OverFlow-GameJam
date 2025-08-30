using System;
using UnityEngine;

public class OutputTextEnableCorrectText : MonoBehaviour
{
    [SerializeField] private Typer typer;
    [SerializeField] private GameObject correctTextEffectGO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        typer.OnCorrectWordCompleted += (object sender, Typer.OnCorrectWordCompletedEventArgs e) =>
        {
            correctTextEffectGO.SetActive(true);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

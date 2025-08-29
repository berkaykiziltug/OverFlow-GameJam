using TMPro;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Typer : MonoBehaviour
{
    [SerializeField] private WordBank wordBank;
    [SerializeField] private GameObject letterPrefab; 
    [SerializeField] private Transform spawnParent; 
    
    public TextMeshProUGUI wordOutput;

    private string remainingWord;
    private string currentWord = string.Empty;
    public Action OnWordCompletedSuccess;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetCurrentWord();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void SetCurrentWord()
    {
        currentWord = wordBank.GetWord();
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            if (keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            }
        }
    }

    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            RemoveLetter();
            if (IsWordComplete())
            {
                OnWordCompletedSuccess?.Invoke();
                SpawnFallingLetters();
                SetCurrentWord();
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }

    private void SpawnFallingLetters()
    {
        Debug.Log("Spawning letters for word: " + currentWord);
        
        // Get the UI text position and convert to world position
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(wordOutput.transform.position);
        worldPos.z = -1f; 
        
        for (int i = 0; i < currentWord.Length; i++)
        {
            
            GameObject letterObj = Instantiate(letterPrefab, spawnParent);
            
            
            TextMeshPro letterText = letterObj.GetComponent<TextMeshPro>();
            letterText.text = currentWord[i].ToString();
            letterText.fontSize = 3f; 
            letterText.color = Random.ColorHSV(0f, 1f, 0.7f, 1f, 0.8f, 1f);
            
            
            float spacing = 0.01f; 
            Vector3 spawnPos = worldPos + new Vector3(
                Random.Range(-0.1f, 0.1f), 
                Random.Range(0f, 0.2f),    
                0
            );
            letterObj.transform.position = spawnPos;
            
            
            Rigidbody2D rb = letterObj.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(
                Random.Range(-.1f, .1f),     
                Random.Range(.3f, .6f)       
            ), ForceMode2D.Impulse);
            
            
            rb.AddTorque(Random.Range(-20f, 20f));
        }
    }
}
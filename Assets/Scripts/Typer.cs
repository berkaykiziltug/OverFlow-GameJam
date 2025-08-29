using TMPro;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Random = UnityEngine.Random;

public class Typer : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;
    [SerializeField] private float impulseAmount;
    
    
    [SerializeField] private WordBank wordBank;
    [SerializeField] private GameObject letterPrefab; 
    [SerializeField] private Transform spawnParent;
    [SerializeField] private float letterDestroyTime;
    
    public TextMeshProUGUI wordOutput;

    private string remainingWord;
    private string currentWord = string.Empty;
    public Action OnWordCompletedSuccess;
    
    private List<Collider2D> spawnedWordColliders = new List<Collider2D>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetCurrentWord();
        ImpulseScreenShake();
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
        else
        {
            ImpulseScreenShake();
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
        
        spawnedWordColliders.Clear();
        
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
            Vector3 spawnPos = worldPos;
            letterObj.transform.position = spawnPos;
            
            
            Rigidbody2D rb = letterObj.GetComponent<Rigidbody2D>();
            Collider2D collider = letterObj.GetComponent<Collider2D>();

            collider.isTrigger = true;
            rb.AddForce(new Vector2(
                Random.Range(-.02f, .02f),     
                Random.Range(-.02f, .02f)       
            ), ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-20f, 20f));
            
            spawnedWordColliders.AddRange(letterObj.GetComponents<Collider2D>());
            StartCoroutine(DisableIsTrigger());
            
            Destroy(letterObj, letterDestroyTime);
        }
    }

    private IEnumerator DisableIsTrigger()
    {
        yield return new WaitForSeconds(.05f);
        for (int i = 0; i < spawnedWordColliders.Count; i++)
        {
            spawnedWordColliders[i].isTrigger = false;
        }
    }

    private void ImpulseScreenShake()
    {
        cinemachineImpulseSource.GenerateImpulse(impulseAmount);
    }
}
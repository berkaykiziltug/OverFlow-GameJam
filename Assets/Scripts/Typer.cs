using TMPro;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Random = UnityEngine.Random;
using System.Globalization;
using EasyTextEffects;

public class Typer : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;
    [SerializeField] private float impulseAmount;
    
    [SerializeField] private TextEffect textEffect;
    [SerializeField] private WordBank wordBank;
    [SerializeField] private GameObject letterPrefab; 
    [SerializeField] private Transform spawnParent;
    [SerializeField] private float letterDestroyTime;
    [SerializeField] private ParticleSystem particleSystem;
    
    public TextMeshProUGUI wordOutput;

    private string remainingWord;
    private string currentWord = string.Empty;
    
    private List<Collider2D> spawnedWordCollidersList = new List<Collider2D>();
    private List<GameObject> spawnedLetters = new List<GameObject>();
    private bool currentLetterIsWrong = false;
    private bool canCollide;

    
    private int currentIndex = 0; //Typed letter index.

    public EventHandler<OnNewWordSpawnedEventArgs> OnNewWordSpawned;
    public EventHandler<OnCorrectWordCompletedEventArgs> OnCorrectWordCompleted;

    private string previousWord = "";

    public class OnCorrectWordCompletedEventArgs : EventArgs
    {
        public string correctWord;
    }
    public class OnNewWordSpawnedEventArgs : EventArgs
    {
        public string word;
    }

    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetCurrentWord();
        // wordOutput.text = "<color=green>C</color><color=red>A</color><color=#F5F5F5>T</color>";
        ImpulseScreenShake();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void SetCurrentWord()
    {
        currentWord = wordBank.GetWord().ToUpper(CultureInfo.InvariantCulture);
        Debug.Log($"Setting current word to {currentWord}");
        currentIndex = 0;
        currentLetterIsWrong = false; // reset for new word
        UpdateWordOutput();
        OnNewWordSpawned?.Invoke(this, new OnNewWordSpawnedEventArgs() { word = currentWord });
    }

    public string GetCurrentWord()
    {
        return currentWord;
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
    }
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Return))
        {
            if (currentLetterIsWrong)
            {
                //Reset player input so player can type again.
                currentLetterIsWrong = false;
                UpdateWordOutput(false);
            }
            return;
        }

        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;
            if (keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            }
        }
    }
    private void ResetCurrentLetter()
    {
        // Only do something if the current letter is wrong (red)
        UpdateWordOutput(false); 
    }
   
    
    private void EnterLetter(string typedLetter)
    {
        if (currentIndex >= currentWord.Length)
            return;
        
        typedLetter = typedLetter.ToUpper(CultureInfo.InvariantCulture);

        if (currentLetterIsWrong)
        {
            //Need to reset to type further. (Press backspace to delete basically)
            return;
        }

        if (typedLetter == currentWord[currentIndex].ToString())
        {
            // Correct letter
            currentIndex++;
            UpdateWordOutput(false);
        }
        else
        {
            // Wrong letter lock it
            currentLetterIsWrong = true;
            UpdateWordOutput(true);
            ImpulseScreenShake();
        }

        // Check if word is complete
        if (currentIndex == currentWord.Length)
        {
            previousWord = currentWord;
            Debug.Log("Sending completed word: " + previousWord); // Add this debug line
            OnCorrectWordCompleted?.Invoke(this, new OnCorrectWordCompletedEventArgs(){correctWord = previousWord});
            SpawnFallingLetters();
            SetCurrentWord(); // reset currentIndex & flag
        }
    }
    
    private void UpdateWordOutput(bool wrong = false)
    {
        string result = "";

        for (int i = 0; i < currentWord.Length; i++)
        {
            if (i < currentIndex)
            {
                // Correct letters are green
                result += $"<color=green>{currentWord[i]}</color>";
            }
            else if (i == currentIndex && wrong)
            {
                // Wrong letter is red
                result += $"<color=red>{currentWord[i]}</color>";
            }
            else
            {
                //The rest of the letters that are not yet typed are grayish.
                result += $"<color=#F8F8FF>{currentWord[i]}</color>";
            }
        }
        wordOutput.text = result;
      
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
        List<GameObject> lettersForThisWord = new List<GameObject>();
        Debug.Log("Spawning letters for word: " + currentWord);
        
        spawnedWordCollidersList.Clear();
        spawnedLetters.Clear();
        
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(wordOutput.transform.position);
        worldPos.z = -1f; 
        
        for (int i = 0; i < currentWord.Length; i++)
        {
            
            GameObject letterObj = Instantiate(letterPrefab, spawnParent);
            
            
            TextMeshPro letterText = letterObj.GetComponentInChildren<TextMeshPro>();
            letterText.text = currentWord[i].ToString();
            letterText.fontSize = 5f; 
            letterText.color = Random.ColorHSV(0f, 1f, 0.7f, 1f, 0.8f, 1f);
            
            
            float spacing = 0.06f;
            Vector3 spawnPos = worldPos;
            letterObj.transform.position = spawnPos;
            
            
            Rigidbody2D rb = letterObj.GetComponent<Rigidbody2D>();
            Collider2D collider = letterObj.GetComponent<Collider2D>();
            collider.isTrigger = true;

            rb.AddForce(new Vector2(
                Random.Range(-.05f, .05f),     
                Random.Range(-.05f, .05f)       
            ), ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-30f, 30f));
            
            spawnedWordCollidersList.AddRange(letterObj.GetComponents<Collider2D>());
            
            StartCoroutine(DisableIsTrigger(collider));
            
            // Destroy(letterObj, letterDestroyTime);
            spawnedLetters.Add(letterObj);
            lettersForThisWord.Add(letterObj);
           
        }
        
        StartCoroutine(DestroyLettersOneByOne(lettersForThisWord));
    }
    
    private IEnumerator DisableIsTrigger(Collider2D col)
    {
        yield return new WaitForSeconds(0.06f); 
        if (col != null) 
            col.isTrigger = false;
    }

    private void ImpulseScreenShake()
    {
        cinemachineImpulseSource.GenerateImpulse(impulseAmount);
    }
    
    private IEnumerator DestroyLettersOneByOne(List<GameObject> letters)
    {
        if (letters.Count == 0) yield break;

        float minDelay = 3f; 
        float maxDelay = letterDestroyTime + minDelay; 

        for (int i = 0; i < letters.Count; i++)
        {
            
            float t = i / (float)(letters.Count - 1);

            
            float waitTime = Mathf.Lerp(minDelay, maxDelay, t);

            yield return new WaitForSeconds(waitTime);

            if (letters[i] != null)
            {
                if (particleSystem != null)
                {
                    ParticleSystem particle = Instantiate(particleSystem,letters[i].transform.position, Quaternion.identity);
                    particle.Play();
                    Destroy(particle.gameObject, 
                        particle.main.duration + particle.main.startLifetime.constantMax);
                }
                Destroy(letters[i]);
            }
                
        }

        letters.Clear();
    }

}
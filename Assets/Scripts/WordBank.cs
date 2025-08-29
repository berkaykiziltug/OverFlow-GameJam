using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class WordBank : MonoBehaviour
{
    private List<string> originalWordsList = new List<string>()
    {
        "berkay",
        "are",
        "tasty",
        "goober",
        "simple",
        "hard",
        "enjoy",
        "cooked",
        "developed",
        "apple",
        "ghost",
        "river",
        "flame",
        "quick",
        "storm",
        "velvet",
        "happy",
        "night",
        "zebra",
        "chair",
        "stone",
        "music",
        "dream",
        "tiger",
        "light",
        "frost",
        "honey",
        "cloud",
        "spark",
        "dragon",
        "peace",
        "tower",
        "jungle",
        "magic",
        "crystal",
        "blade",
        "ocean",
        "wolf",
        "galaxy",
        "lemon",
        "shadow",
        "brave",
        "orbit",
        "silver",
        "candy",
        "thunder",
        "island",
        "eagle",
        "forest",
        "prism",
        "comet",
        "crown",
        "whisper",
        "mirror",
        "phoenix",
        "secret",
        "planet",
        "ember"
    };

    private List<string> workingWordsList = new List<string>();

    private void Awake()
    {
        workingWordsList.AddRange(originalWordsList);
        
        ShuffleWords(workingWordsList);
        
        ConvertToLower(workingWordsList);
        
    }

    private void ShuffleWords(List<string> wordsList)
    {
        for (int i = 0; i < wordsList.Count; i++)
        {
            int randomIndex = Random.Range(i, wordsList.Count);
            string temporary = wordsList[i];
            wordsList[i]= wordsList[randomIndex];
            wordsList[randomIndex] = temporary;
            
        }
    }

    private void ConvertToLower(List<string> wordsList)
    {
        for (int i = 0; i < wordsList.Count; i++)
        {
            wordsList[i] = wordsList[i].ToLower();
        }
    }

    public string GetWord()
    {
        string newWord = string.Empty;

        if (workingWordsList.Count != 0)
        {
            newWord = workingWordsList.Last();
            workingWordsList.Remove(newWord);
        }

        return newWord;
    }
}

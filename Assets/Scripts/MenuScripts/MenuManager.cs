using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private const string GAME_SCENE = "GameScene";

    public void StartGame()
    {
        SceneManager.LoadScene(GAME_SCENE);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

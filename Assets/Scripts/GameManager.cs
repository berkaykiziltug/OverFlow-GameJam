using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    [SerializeField] private Typer typer;
    [SerializeField] private TextMeshProUGUI timeText;

    private int score;

    private float currentTime;
    // private float maxTime = 60;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        
        currentTime = 0;
    }

    private void Letter_OnLetterDestroyed(Vector3 position)
    {
       
    }


    // Update is called once per frame
    void Update()
    {
            currentTime += Time.deltaTime;
            if (timeText != null)
            {
                timeText.text = FormatTime(currentTime);
            }
    }
    /// <summary>
    /// Converts a float time in seconds to a string in HH:MM:SS format.
    /// </summary>
    private string FormatTime(float time)
    {
        int hours = Mathf.FloorToInt(time / 3600f);
        int minutes = Mathf.FloorToInt((time % 3600) / 60f);
        int seconds = Mathf.FloorToInt(time % 60);

        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
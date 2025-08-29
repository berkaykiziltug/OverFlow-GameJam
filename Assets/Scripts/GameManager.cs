using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    [SerializeField] private Typer typer;
    [SerializeField] private TextMeshProUGUI timeText;

    private int score;

    private float currentTime;
    private float maxTime = 40;

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

        typer.OnWordCompletedSuccess += Typer_OnWordCompleteSuccess;
        currentTime = maxTime;
    }

    private void Typer_OnWordCompleteSuccess()
    {
        //Run whatever logic you want on word success.
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            if (timeText != null)
            {
                timeText.text = Mathf.Ceil(currentTime).ToString();
            }

            if (currentTime <= 0)
            {
                Debug.Log("TIME IS UP YOU LOSE");
                // You can call a GameOver function here
            }
        }
    }
}
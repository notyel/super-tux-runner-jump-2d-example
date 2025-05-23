using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float startTime = 280f; // Tiempo en segundos (4 minutos con 40 segundos)
    private float currentTime;
    private bool isRunning = false;

    [SerializeField] private TextMeshProUGUI timerText;

    public GameManager gameManager;

    void Start()
    {
        currentTime = startTime;
        isRunning = true;
        UpdateTimerUI();
    }

    void Update()
    {
        if (!isRunning || gameManager.isGameOver || !gameManager.isGameStarted)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            isRunning = false;
            gameManager.isGameOver = true;
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}

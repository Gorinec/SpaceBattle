using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

{
    [Header("Таймер")]
    public float timeRemaining = 120f; 
    public TMP_Text timerText;

    private bool isGameOver = false;

    void Start()
    {
        UpdateTimerUI();
    }

    void Update()
    {
        if (isGameOver) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            timeRemaining = 0;
            WinGame();
        }
    }

    void WinGame()
    {
        isGameOver = true;
        SceneManager.LoadScene(2); // Сцена победы
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    
    public void PlayerDied()
    {
        SceneManager.LoadScene(3); // Сцена поражения (индекс 3)
    }
}
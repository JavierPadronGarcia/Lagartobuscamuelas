using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Temporizador")]
    public float gameDuration = 120f;
    private float timeRemaining;
    private bool timerRunning = false;

    [Header("Estadisticas")]
    public int health = 3;
    public int hints = 3;
    public int minesLeft;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI minesLeftText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI hintsText;

    [Header("Referencias")]
    public TeethFieldManager fieldManager;

    void Start()
    {
        if (fieldManager == null)
        {
            Debug.LogError("TeethFieldManager not assigned in GameManager.");
            return;
        }

        minesLeft = fieldManager.numberOfBombs;
        timeRemaining = gameDuration;
        timerRunning = true;
        UpdateUI();
    }

    void Update()
    {
        if (timerRunning)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                timerRunning = false;
                GameOver();
            }

            UpdateTimerUI();
        }
    }

    void UpdateUI()
    {
        UpdateTimerUI();
        minesLeftText.text = "Bombas restantes: " + minesLeft;
        healthText.text = "Vida: " + health;
        hintsText.text = "Pistas: " + hints;
    }

    void UpdateTimerUI()
    {
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = "Tiempo: " + seconds;
    }

    void GameOver()
    {
        //Game Over
    }

    public void UpdateMineCount(int change)
    {
        minesLeft += change;
        minesLeftText.text = "Bombas restantes: " + minesLeft;
    }

    public void LoseHealth()
    {
        health--;
        healthText.text = "Vida: " + health;
        if (health <= 0)
        {
            GameOver();
            timerRunning = false;
        }
    }

    public void UseHint()
    {
        if (hints > 0)
        {
            hints--;
            hintsText.text = "Pistas: " + hints;
            // Pistas
        }
    }
}
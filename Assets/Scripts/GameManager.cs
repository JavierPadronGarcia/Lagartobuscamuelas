using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;

public class GameManager : MonoBehaviour
{
    [Header("Temporizador")]
    public float gameDuration = 120f;
    private float timeRemaining;
    private bool timerRunning = false;

    [Header("Estadisticas")]
    public int health = 3;
    public int hints = 3;
    public int minesLeft = 5;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI minesLeftText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI hintsText;
    public TextMeshProUGUI resultText;
    public GameObject canvasGameOver;

    [Header("Referencias")]
    public TeethFieldManager fieldManager;

    [Header("Input Action")]
    public InputActionReference highlightAction;

    private bool gameEnded = false;

    public bool comprobar = false; 

    void Start()
    {
        if (fieldManager == null)
        {
            Debug.LogError("TeethFieldManager not assigned in GameManager.");
            return;
        }

        health = PlayerPrefs.GetInt("Vidas");
        hints = PlayerPrefs.GetInt("Pistas");
        minesLeft = PlayerPrefs.GetInt("Bombas");
        gameDuration = PlayerPrefs.GetFloat("Tiempo");
        timeRemaining = gameDuration;
        timerRunning = true;
        UpdateUI();
    }

    private void Awake()
    {
        if (highlightAction != null)
        {
            highlightAction.action.performed += ctx => UseHint();
            highlightAction.action.Enable();
        }
    }

    private void OnDestroy()
    {
        if (highlightAction != null)
        {
            highlightAction.action.performed -= ctx => UseHint();
        }
    }

    void Update()
    {
        if (timerRunning)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0f) {
                GameOver(false);
                print("Acabo tiempo");
            }

            if (health == 0) {
                GameOver(false);
                print("Sin vidas");
            }

            UpdateTimerUI();

            UpdateUI();

            if(comprobar)  CheckWinCondition();
        }
        // Pistas con P
        if (Input.GetKeyDown(KeyCode.P))
        {
            UseHint();
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

    public void CheckWinCondition() {
        int totalSafeTeeth = (fieldManager.rows * fieldManager.cols) - fieldManager.numberOfBombs;
        int revealedSafeTeeth = 0;

        foreach (Tooth t in fieldManager.allTeeth) {
                if (t.isRevealed && !t.isMine)
                    revealedSafeTeeth++;
        }

        if (revealedSafeTeeth == totalSafeTeeth) {
            GameOver(true);
            print("Rompiste los dientes sanos");
        }
    }

    public void GameOver(bool won) {
        if (gameEnded) return; // evitar múltiples llamadas
        gameEnded = true;
        timerRunning = false;
        timeRemaining = 0f;

        // Guardar el estado de victoria/derrota en una variable accesible globalmente
        GameState.isWin = won;

        if (GameState.isWin) {
            resultText.text = "¡Has ganado!";
        } else {
            resultText.text = "Has perdido.";
        }

        canvasGameOver.SetActive(true);
    }

    public void UpdateMineCount(int change)
    {
        minesLeft += change;
        minesLeftText.text = "Bombas restantes: " + minesLeft;
    }

    public void LoseHealth() {
        health--;
        healthText.text = "Vida: " + health;
    }

    public void UseHint()
    {
        if (hints <= 0) return;

        // Get all unrevealed, non-mine teeth
        List<Tooth> validTeeth = new List<Tooth>();

        foreach (Tooth t in fieldManager.allTeeth)
        {
            if (!t.isMine && t.highlight != null && !t.isRevealed)
                validTeeth.Add(t);
        }

        if (validTeeth.Count == 0)
        {
            Debug.Log("No valid teeth to highlight.");
            return;
        }

        // Pick one at random and highlight it
        Tooth chosen = validTeeth[Random.Range(0, validTeeth.Count)];
        chosen.highlight.UseHint();

        hints--;
        hintsText.text = "Pistas: " + hints;
    }
}
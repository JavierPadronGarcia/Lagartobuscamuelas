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
    public int minesLeft;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI minesLeftText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI hintsText;

    [Header("Referencias")]
    public TeethFieldManager fieldManager;

    [Header("Input Action")]
    public InputActionReference highlightAction;

    private bool gameEnded = false;

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
                timeRemaining = 0f;
                timerRunning = false;
                GameOver(false);
            }

            UpdateTimerUI();
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
        int totalSafeTeeth = 0;
        int revealedSafeTeeth = 0;

        foreach (Tooth t in fieldManager.allTeeth) {
            if (!t.isMine) {
                totalSafeTeeth++;
                if (t.isRevealed)
                    revealedSafeTeeth++;
            }
        }

        if (revealedSafeTeeth == totalSafeTeeth) {
            GameOver(true);
        }
    }

    public void GameOver(bool won) {
        if (gameEnded) return; // evitar múltiples llamadas
        gameEnded = true;
        timerRunning = false;

        // Guardar el estado de victoria/derrota en una variable accesible globalmente
        GameState.isWin = won;

        // Cargar la escena aditiva donde se mostrará el mensaje
        SceneManager.LoadScene("FinDelJuego", LoadSceneMode.Additive);
    }

    public void UpdateMineCount(int change)
    {
        minesLeft += change;
        minesLeftText.text = "Bombas restantes: " + minesLeft;
    }

    public void LoseHealth() {
        health--;
        healthText.text = "Vida: " + health;
        if (health <= 0) {
            GameOver(false);
        }
    }

    public void UseHint()
    {
        if (hints <= 0) return;

        // Get all unrevealed, non-mine teeth
        List<Tooth> validTeeth = new List<Tooth>();

        foreach (Tooth t in fieldManager.allTeeth)
        {
            if (!t.isMine && t.highlight != null)
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
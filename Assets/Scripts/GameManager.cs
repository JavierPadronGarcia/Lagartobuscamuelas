using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Temporizador")]
    public float gameDuration = 120f;
    public float timeRemaining;
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
    public GameDataShow handData;
    public GameDataShow pizarraData;

    [Header("UI Notificaciones")]
    public TextMeshProUGUI timeLeftText;

    [Header("Fades")]
    public Image fadeImage;
    public float fadeDuration = 2f;

    [Header("Referencias")]
    public TeethFieldManager fieldManager;
    public GameObject XROrigin;
    public Transform finishGamePosition;

    [Header("Input Action")]
    public InputActionReference highlightAction;

    private bool gameEnded = false;
    private bool thirtySecondsShown = false;

    public bool comprobar = false;

    void Start()
    {
        if (fieldManager == null)
        {
            //Debug.LogError("TeethFieldManager not assigned in GameManager.");
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

            if (timeRemaining <= 0f)
            {
                GameOver(false);
                print("Acabo tiempo");
            }
            else if (timeRemaining <= 30f && !thirtySecondsShown)
            {
                thirtySecondsShown = true;
                timeLeftText.gameObject.SetActive(true);
                Invoke("HideTimeLeftText", 2f);
            }

            UpdateTimerUI();

            UpdateUI();

            if (comprobar) CheckWinCondition();
        }
        // Pistas con P
        if (Input.GetKeyDown(KeyCode.P))
        {
            UseHint();
        }
    }

    private void HideTimeLeftText()
    {
        timeLeftText.gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        pizarraData.UpdateUI();
        handData.UpdateUI();
        //UpdateTimerUI();
        //minesLeftText.text = "Bombas restantes: " + minesLeft;
        //healthText.text = "Vida: " + health;
        //hintsText.text = "Pistas: " + hints;
    }

    void UpdateTimerUI()
    {
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = "Tiempo: " + seconds;
    }

    public void CheckWinCondition()
    {
        int totalSafeTeeth = (fieldManager.rows * fieldManager.cols) - fieldManager.numberOfBombs;
        int revealedSafeTeeth = 0;

        foreach (Tooth t in fieldManager.allTeeth)
        {
            if (t.isRevealed && !t.isMine)
                revealedSafeTeeth++;
        }

        if (revealedSafeTeeth == totalSafeTeeth)
        {
            GameOver(true);
            print("Rompiste los dientes sanos");
        }
    }

    public void GameOver(bool won)
    {
        if (gameEnded) return; // evitar múltiples llamadas
        gameEnded = true;
        timerRunning = false;
        timeRemaining = 0f;

        // Guardar el estado de victoria/derrota en una variable accesible globalmente
        GameState.isWin = won;

        if (GameState.isWin)
        {
            AudioManager.instance.PlaySFX("Victory");
            resultText.text = "¡Has ganado!";
        }
        else
        {
            AudioManager.instance.PlaySFX("Lose");
            resultText.text = "Has perdido.";
        }

        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeImageCoroutine(true));
        yield return new WaitForSeconds(fadeDuration + 0.2f);
        XROrigin.transform.SetPositionAndRotation(finishGamePosition.position, finishGamePosition.rotation);
        canvasGameOver.SetActive(true);
        StartCoroutine(FadeImageCoroutine(false));
    }

    private IEnumerator FadeImageCoroutine(bool fadeIn = true)
    {
        float startAlpha = fadeIn ? 0f : 1f;
        float endAlpha = fadeIn ? 1f : 0f;
        float elapsedTime = 0f;

        Color color = fadeImage.color;
        color.a = startAlpha;
        fadeImage.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }

        // Asegurar valor final exacto
        color.a = endAlpha;
        fadeImage.color = color;
    }


    public void UpdateMineCount(int change)
    {
        minesLeft += change;
        minesLeftText.text = "Bombas restantes: " + minesLeft;
    }

    public void LoseHealth()
    {
        health--;
        if (health <= 0)
        {
            GameOver(false);
            print("Sin vidas");
            health = 0;
        }
        healthText.text = "Vidas: " + health;
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
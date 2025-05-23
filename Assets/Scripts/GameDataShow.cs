using TMPro;
using UnityEngine;

#nullable enable

public class GameDataShow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI? timerText;
    [SerializeField] private TextMeshProUGUI? minesLeftText;
    [SerializeField] private TextMeshProUGUI? healthText;
    [SerializeField] private TextMeshProUGUI? hintsText;

    private GameManager? gameManager;

    private void Awake()
    {
        gameManager ??= GameObject.Find("GameManager")?.GetComponent<GameManager>();

        if (gameManager != null) UpdateUI();
    }

    public void UpdateUI()
    {
        if (!gameObject.activeSelf || gameManager == null || timerText == null || hintsText == null || healthText == null || minesLeftText == null)
            return;

        timerText.text = Mathf.CeilToInt(gameManager.timeRemaining).ToString();
        hintsText.text = gameManager.hints.ToString();
        healthText.text = gameManager.health.ToString();
        minesLeftText.text = gameManager.minesLeft.ToString();
    }
}
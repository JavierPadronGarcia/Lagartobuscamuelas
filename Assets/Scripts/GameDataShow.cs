using TMPro;
using UnityEngine;

public class GameDataShow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI minesLeftText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI hintsText;

    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Awake()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        timerText.text = Mathf.CeilToInt(gameManager.timeRemaining).ToString();
        hintsText.text = gameManager.hints.ToString();
        healthText.text = gameManager.health.ToString();
        minesLeftText.text = gameManager.minesLeft.ToString();
    }

}

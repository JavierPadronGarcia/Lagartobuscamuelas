using UnityEngine;
using TMPro;

public class GameSettingsPanel : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bombsText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI hintsText;

    private float[] timeOptions = { 60f, 90f, 120f, 180f, 240f, 270f, 300f };
    private int[] bombsOptions = { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
    private int[] livesOptions = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private int[] hintsOptions = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    private int currentTimeIndex = 2;
    private int currentBombsIndex = 0;
    private int currentLivesIndex = 2;
    private int currentHintsIndex = 2;

    private void Start()
    {
        UpdateUI();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Tiempo", timeOptions[currentTimeIndex]);
        PlayerPrefs.SetInt("Bombas", bombsOptions[currentBombsIndex]);
        PlayerPrefs.SetInt("Vidas", livesOptions[currentLivesIndex]);
        PlayerPrefs.SetInt("Pistas", hintsOptions[currentHintsIndex]);

        PlayerPrefs.Save();
    }

    // Flechas
    public void LessTime() { currentTimeIndex = (currentTimeIndex - 1 + timeOptions.Length) % timeOptions.Length; UpdateUI(); }
    public void MoreTime() { currentTimeIndex = (currentTimeIndex + 1) % timeOptions.Length; UpdateUI(); }

    public void LessBombs() { currentBombsIndex = (currentBombsIndex - 1 + bombsOptions.Length) % bombsOptions.Length; UpdateUI(); }
    public void MoreBombs() { currentBombsIndex = (currentBombsIndex + 1) % bombsOptions.Length; UpdateUI(); }

    public void LessLives() { currentLivesIndex = (currentLivesIndex - 1 + livesOptions.Length) % livesOptions.Length; UpdateUI(); }
    public void MoreLives() { currentLivesIndex = (currentLivesIndex + 1) % livesOptions.Length; UpdateUI(); }

    public void LessHints() { currentHintsIndex = (currentHintsIndex - 1 + hintsOptions.Length) % hintsOptions.Length; UpdateUI(); }
    public void MoreHints() { currentHintsIndex = (currentHintsIndex + 1) % hintsOptions.Length; UpdateUI(); }

    // Dificultades
    public void SetEasy()
    {
        currentTimeIndex = GetClosestIndex(timeOptions, 120f);
        currentBombsIndex = GetClosestIndex(bombsOptions, 5);
        currentLivesIndex = GetClosestIndex(livesOptions, 3);
        currentHintsIndex = GetClosestIndex(hintsOptions, 3);
        UpdateUI();
    }

    public void SetNormal()
    {
        currentTimeIndex = GetClosestIndex(timeOptions, 180f);
        currentBombsIndex = GetClosestIndex(bombsOptions, 7);
        currentLivesIndex = GetClosestIndex(livesOptions, 2);
        currentHintsIndex = GetClosestIndex(hintsOptions, 2);
        UpdateUI();
    }

    public void SetHard()
    {
        currentTimeIndex = GetClosestIndex(timeOptions, 240f);
        currentBombsIndex = GetClosestIndex(bombsOptions, 10);
        currentLivesIndex = GetClosestIndex(livesOptions, 1);
        currentHintsIndex = GetClosestIndex(hintsOptions, 1);
        UpdateUI();
    }

    // Actualizar UI
    private void UpdateUI()
    {
        timeText.text = timeOptions[currentTimeIndex].ToString("F0");
        bombsText.text = bombsOptions[currentBombsIndex].ToString();
        livesText.text = livesOptions[currentLivesIndex].ToString();
        hintsText.text = hintsOptions[currentHintsIndex].ToString();
        SaveSettings();
    }

    private int GetClosestIndex<T>(T[] array, T value) where T : System.IComparable
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].CompareTo(value) == 0)
                return i;
        }
        return 0;
    }
}

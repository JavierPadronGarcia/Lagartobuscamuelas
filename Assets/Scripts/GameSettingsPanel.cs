using UnityEngine;
using UnityEngine.UI;

public class GameSettingsPanel : MonoBehaviour
{
    public Text timeText;
    public Text bombsText;
    public Text livesText;
    public Text hintsText;

    private float[] timeOptions = { 60f, 90f, 120f, 180f, 240f };
    private int[] bombsOptions = { 5, 6, 7, 8, 9, 10 };
    private int[] livesOptions = { 1, 2, 3, 4, 5 };
    private int[] hintsOptions = { 1, 2, 3, 4, 5 };

    private int currentTimeIndex = 0;
    private int currentBombsIndex = 1;
    private int currentLivesIndex = 0;
    private int currentHintsIndex = 1;

    private void Start()
    {
        UpdateUI();
    }

    public void GoToLocalGame()
    {
        PlayerPrefs.SetFloat("Tiempo", timeOptions[currentTimeIndex]);
        PlayerPrefs.SetInt("Bombas", bombsOptions[currentBombsIndex]);
        PlayerPrefs.SetInt("Vidas", livesOptions[currentLivesIndex]);
        PlayerPrefs.SetInt("Pistas", hintsOptions[currentHintsIndex]);

        PlayerPrefs.Save();

        // SCManager.instance.LoadScene("ControllerTutorial");
        // AudioManager.instance.PlayMusic("Platano_Partida_Loop");
        // AudioManager.instance.PlaySFX("Botones");
    }

    // Flechas
    public void LessTime()
    {
        currentTimeIndex = (currentTimeIndex - 1 + timeOptions.Length) % timeOptions.Length;
        UpdateUI();
    }

    public void MoreTime()
    {
        currentTimeIndex = (currentTimeIndex + 1) % timeOptions.Length;
        UpdateUI();
    }

    public void LessBombs()
    {
        currentBombsIndex = (currentBombsIndex - 1 + bombsOptions.Length) % bombsOptions.Length;
        UpdateUI();
    }

    public void MoreBombs()
    {
        currentBombsIndex = (currentBombsIndex + 1) % bombsOptions.Length;
        UpdateUI();
    }

    public void LessLives()
    {
        currentLivesIndex = (currentLivesIndex - 1 + livesOptions.Length) % livesOptions.Length;
        UpdateUI();
    }

    public void MoreLives()
    {
        currentLivesIndex = (currentLivesIndex + 1) % livesOptions.Length;
        UpdateUI();
    }

    public void LessHints()
    {
        currentHintsIndex = (currentHintsIndex - 1 + hintsOptions.Length) % hintsOptions.Length;
        UpdateUI();
    }

    public void MoreHints()
    {
        currentHintsIndex = (currentHintsIndex + 1) % hintsOptions.Length;
        UpdateUI();
    }

    // Dificultades
    public void SetEasy()
    {
        currentTimeIndex = GetClosestIndex(timeOptions, 240f);
        currentBombsIndex = GetClosestIndex(bombsOptions, 5);
        currentLivesIndex = GetClosestIndex(livesOptions, 5);
        currentHintsIndex = GetClosestIndex(hintsOptions, 5);
        UpdateUI();
    }

    public void SetNormal()
    {
        currentTimeIndex = GetClosestIndex(timeOptions, 120f);
        currentBombsIndex = GetClosestIndex(bombsOptions, 7);
        currentLivesIndex = GetClosestIndex(livesOptions, 3);
        currentHintsIndex = GetClosestIndex(hintsOptions, 3);
        UpdateUI();
    }

    public void SetHard()
    {
        currentTimeIndex = GetClosestIndex(timeOptions, 60f);
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

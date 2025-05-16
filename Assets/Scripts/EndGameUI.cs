using UnityEngine;
using TMPro;

public class EndGameUI : MonoBehaviour {
    public TextMeshProUGUI resultText;

    void Start() {
        if (GameState.isWin) {
            resultText.text = "¡Has ganado!";
        } else {
            resultText.text = "Has perdido.";
        }
    }
}
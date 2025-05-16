using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorDeCarga : MonoBehaviour
{
    private AsyncOperation cargaEscena;

    public void PreCargarEscena(string nombreEscena)
    {
        Debug.Log("Iniciando precarga de escena: " + nombreEscena);
        cargaEscena = SceneManager.LoadSceneAsync(nombreEscena);
        cargaEscena.allowSceneActivation = false; // Esperar hasta que tú lo autorices
    }

    public void ActivarEscena()
    {
        if (cargaEscena != null && cargaEscena.progress >= 0.9f)
        {
            Debug.Log("Activando escena precargada.");
            cargaEscena.allowSceneActivation = true;
        }
        else
        {
            Debug.LogWarning("La escena aún no está lista para activarse.");
        }
    }
}

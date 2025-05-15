using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrapper : MonoBehaviour
{
    [Header("Escena embebida")]
    [SerializeField] private string sceneToLoad = "EmbeddedScene";

    [Header("Transición visual")]
    [SerializeField] private GameObject ActivarCanvas;
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    private void Start()
    {
        ActivarCanvas.SetActive(true);
        // Cargar la escena embebida si no está cargada
        if (!SceneManager.GetSceneByName(sceneToLoad).isLoaded)
        {
            SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        }

        // Iniciar fade si hay un CanvasGroup asignado
        if (fadeCanvasGroup != null)
        {
            StartCoroutine(FadeOutCanvasGroup());
        }
    }

    private System.Collections.IEnumerator FadeOutCanvasGroup()
    {
        fadeCanvasGroup.gameObject.SetActive(true);
        fadeCanvasGroup.alpha = 1f;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f;
        ActivarCanvas.SetActive(false);
        //fadeCanvasGroup.gameObject.SetActive(false); // opcional
    }
}


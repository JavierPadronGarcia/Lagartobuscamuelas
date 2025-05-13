using System.Collections;
using UnityEngine;

public class HintHighlight : MonoBehaviour
{
    public int hintCount = 3;                      // Number of hints available
    public float glowDuration = 30f;                // Glow time in seconds
    public Color glowColor = Color.cyan;           // Emission glow color
    public float glowIntensity = 3f;               // Emission brightness
    public KeyCode testKey = KeyCode.P;            // For testing in editor

    private Renderer rend;
    private Material mat;
    private Color originalEmission;
    private bool isGlowing = false;

    private Light pointLight;

    void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;
        originalEmission = mat.GetColor("_EmissionColor");

        // Get the Point Light component from this object or its children
        pointLight = GetComponentInChildren<Light>();
        if (pointLight != null)
            pointLight.enabled = false;

        DisableEmission(); // Make sure emission starts off
    }

    public void UseHint()
    {
        //if (hintCount > 0 && !isGlowing)
        //{
        //    hintCount--;
        //    StartCoroutine(GlowEffect());
        //}
        if (isGlowing) return;

        isGlowing = true;
        EnableEmission(glowColor * glowIntensity);

        if (pointLight != null)
            pointLight.enabled = true;
    }

    //private IEnumerator GlowEffect()
    //{
    //    isGlowing = true;

    //    // Enable emission and light
    //    EnableEmission(glowColor * glowIntensity);
    //    if (pointLight != null)
    //        pointLight.enabled = true;

    //    yield return new WaitForSeconds(glowDuration);

    //    // Disable emission and light
    //    DisableEmission();
    //    if (pointLight != null)
    //        pointLight.enabled = false;

    //    isGlowing = false;
    //}

    private void EnableEmission(Color emissionColor)
    {
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", emissionColor);
    }

    private void DisableEmission()
    {
        mat.SetColor("_EmissionColor", Color.black);
    }
}

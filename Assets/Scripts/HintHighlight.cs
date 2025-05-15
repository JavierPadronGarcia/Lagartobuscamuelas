using System.Collections;
using UnityEngine;

public class HintHighlight : MonoBehaviour
{
    public float glowDuration = 30f;
    public Color glowColor = Color.yellow;
    public float glowIntensity = 3f;
    public float maxGlowIntensity = 0f;
    public float minGlowIntensity = -9f;
    public float pulseSpeed = 2f;

    private Renderer rend;
    private Material mat;
    private Color originalEmission;
    private bool isGlowing = false;

    private Light pointLight;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;
        DisableEmission();
    }

    //public void UseHint()
    //{
    //    if (isGlowing) return;
    //    StartCoroutine(GlowEffect());
    //}

    public void UseHint()
    {
        if (isGlowing) return;

        isGlowing = true;
        EnableEmission(glowColor * glowIntensity);

        StartCoroutine(GlowEffect());

        if (pointLight != null)
            pointLight.enabled = true;
    }

    private IEnumerator GlowEffect()
    {
        isGlowing = true;

        float elapsed = 0f;

        while (elapsed < glowDuration)
        {
            // PingPong entre 0 y 1
            float t = Mathf.PingPong(Time.time * pulseSpeed, 1f);
            float intensity = Mathf.Lerp(minGlowIntensity, maxGlowIntensity, t);
            EnableEmission(glowColor * intensity);

            elapsed += Time.deltaTime;
            yield return null;
        }

        DisableEmission();
        isGlowing = false;
    }

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

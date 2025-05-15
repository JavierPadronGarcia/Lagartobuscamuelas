using UnityEngine;

public class FlagWindController : MonoBehaviour
{
    public Cloth cloth;
    public Vector3 localWindDirection = new Vector3(0, 0, 10); // wind blowing along local Z

    void Update()
    {
        // Convert local wind vector into world space
        Vector3 worldWind = transform.rotation * localWindDirection;
        cloth.externalAcceleration = worldWind;
    }
}

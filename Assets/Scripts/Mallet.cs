using UnityEngine;

public class Mallet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Tooth tooth = other.GetComponent<Tooth>();

        if (tooth != null)
        {
            tooth.Reveal();
        }
    }
}

using UnityEngine;

public class Mallet : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        Tooth tooth = other.gameObject.GetComponent<Tooth>();

        if (tooth != null)
        {
            tooth.Reveal();
        }
    }
}

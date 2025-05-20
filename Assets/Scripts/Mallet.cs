using UnityEngine;

public class Mallet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Tooth tooth = other.gameObject.GetComponent<Tooth>();

        if (other.CompareTag("Tooth"))
        {
            tooth.Reveal();
        }
    }
}

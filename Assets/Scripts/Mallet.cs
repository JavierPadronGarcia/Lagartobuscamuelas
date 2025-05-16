using UnityEngine;

public class Mallet : MonoBehaviour
{
    void OnTriggerEntered(Collision other)
    {
        Tooth tooth = other.gameObject.GetComponent<Tooth>();

        if (tooth != null)
        {
            tooth.Reveal();
            Debug.Log(tooth.transform.GetChild(2).name);
            tooth.transform.GetChild(2).GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}

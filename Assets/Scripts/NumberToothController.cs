using UnityEngine;

public class NumberToothController : MonoBehaviour
{
    public void StartAnimation()
    {
        GetComponent<Animator>().SetTrigger("Appear");

        if (transform.parent.GetComponent<Tooth>().isMine)
        {
            BroadcastMessage("ExplodeBomb");
        }
    }
}

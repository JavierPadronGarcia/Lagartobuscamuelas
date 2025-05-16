using UnityEngine;

public class NumberToothController : MonoBehaviour
{
    public void StartAnimation()
    {
        GetComponent<Animator>().SetTrigger("Appear");
    }
}

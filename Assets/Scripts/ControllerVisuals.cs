using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ControllerVisuals : MonoBehaviour
{
    [SerializeField] InteractorHandedness handedness;

    public void HideController(InteractorHandedness hand)
    {

        if ((hand.Equals(InteractorHandedness.Left) && handedness.Equals(InteractorHandedness.Left)) ||
            (hand.Equals(InteractorHandedness.Right) && handedness.Equals(InteractorHandedness.Right)))
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void ShowController(InteractorHandedness hand)
    {
        if ((hand.Equals(InteractorHandedness.Left) && handedness.Equals(InteractorHandedness.Left)) ||
           (hand.Equals(InteractorHandedness.Right) && handedness.Equals(InteractorHandedness.Right)))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}

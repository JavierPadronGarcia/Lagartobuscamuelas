using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SetFlag : MonoBehaviour
{
    public FlagType currentMode = FlagType.Yellow;

    [Header("Pools")]
    public FlagPool redFlagPool;
    public FlagPool yellowFlagPool;

    [Header("Raycast")]
    public Transform rayOrigin;
    public float rayDistance = 5f;
    public LayerMask toothLayer;

    [Header("Screen Material")]
    public Material redFlagMaterial;
    public Material yellowFlagMaterial;
    public Renderer screenRenderer;

    private XRGrabInteractable grabInteractable;
    private XRBaseInteractor interactorHoldingGun;
    private InputAction toggleAction;


    private bool isHeld => interactorHoldingGun != null;

    public enum FlagType
    {
        Red,
        Yellow
    }

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    void Update()
    {
        if (!isHeld)
        {
            return;
        }
        else
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.forward * rayDistance, Color.red);
        }
    }

    public void OnTriggerPressed()
    {
        if (interactorHoldingGun != null)
            TrySetOrRemoveFlag();
    }

    public void OnTogglePressed()
    {
        if (interactorHoldingGun != null)
            ToggleMode();
    }
    void OnGrab(SelectEnterEventArgs args)
    {
        interactorHoldingGun = args.interactorObject as XRBaseInteractor;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        interactorHoldingGun = null;
    }

    void TrySetOrRemoveFlag()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayDistance, toothLayer))
        {
            Debug.Log($"Raycast hit: {hit.collider.name}");
            Tooth tooth = hit.collider.GetComponent<Tooth>();
            if (tooth != null)
            {
                Debug.Log("Tooth component found!");
                if (tooth.HasFlag())
                {
                    tooth.RemoveFlag();
                }
                else
                {
                    FlagPool selectedPool = currentMode == FlagType.Red ? redFlagPool : yellowFlagPool;
                    GameObject flag = selectedPool.GetPooledObject();
                    if (flag != null)
                    {
                        Debug.Log("Spawning flag at: " + hit.point);
                        flag.transform.position = hit.point;
                        flag.transform.rotation = Quaternion.LookRotation(hit.normal);
                        tooth.SetFlag(flag);
                    }
                }
            }
        }
    }

    public void ToggleMode()
    {
        currentMode = currentMode == FlagType.Red ? FlagType.Yellow : FlagType.Red;
        UpdateScreenMaterial();
    }

    void UpdateScreenMaterial()
    {
        if (screenRenderer != null)
        {
            screenRenderer.material = currentMode == FlagType.Red ? redFlagMaterial : yellowFlagMaterial;
        }
    }
}

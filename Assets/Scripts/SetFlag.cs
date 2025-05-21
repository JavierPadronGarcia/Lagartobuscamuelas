using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
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

    [Header("Screen Material")]
    public Material redFlagMaterial;
    public Material yellowFlagMaterial;
    public Renderer screenRenderer;

    private XRGrabInteractable grabInteractable;
    private XRBaseInteractor interactorHoldingGun;
    private InputAction toggleAction;

    [Header("Input Action")]
    public InputActionReference toggleModeAction;

    [SerializeField] private GameObject laser;

    public AudioSource changeFlag;
    public AudioSource setFlag;


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
        if (toggleModeAction != null)
        {
            toggleModeAction.action.performed += ctx => OnTogglePressed();
            toggleModeAction.action.Enable();
        }
        laser.SetActive(false);
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
        if (toggleModeAction != null)
        {
            toggleModeAction.action.performed -= ctx => OnTogglePressed();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            TrySetOrRemoveFlag();
        }

        if (Input.GetKeyDown(KeyCode.T)) // Press 'T' to toggle mode
        {
            ToggleMode();
        }
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
        laser.SetActive(true);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        interactorHoldingGun = null;
        laser.SetActive(false);
    }

    void TrySetOrRemoveFlag()
    {
        Debug.Log("TrySetOrRemoveFlag called");

        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            Debug.Log($"Raycast hit: {hit.collider.name}");

            Tooth tooth = hit.transform.parent.GetComponent<Tooth>();
            if (tooth != null)
            {
                setFlag.Play();
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
                        tooth.SetFlag(flag); // Å© positioning is now handled by Tooth
                        Debug.Log("Flag set on tooth!");
                    }
                    else
                    {
                        Debug.LogWarning("No flag available in pool!");
                    }
                }
            }
            else
            {
                Debug.LogWarning("Raycast hit something, but no Tooth component was found.");
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }

    public void ToggleMode()
    {
        changeFlag.Play();
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

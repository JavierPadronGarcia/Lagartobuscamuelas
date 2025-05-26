using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Tooth : MonoBehaviour
{

    public bool isMine = false;
    public bool isRevealed = false;
    public bool isSup = false;
    public int adjacentMines = 0;

    public Material defaultMaterial;
    public Material revealedMaterial;
    public Material mineMaterial;

    public HintHighlight highlight;

    [SerializeField] Transform NumberSpawnPoint;
    [SerializeField] List<GameObject> NumberPrefabs;
    public GameObject currentFlag;
    [SerializeField] private Transform flagSpawnPoint;
    public Transform FlagSpawnPoint => flagSpawnPoint;

    public FlagPool yellowFlag;
    public FlagPool redFlag;

    public AudioSource revealSFX;
    public AudioSource breakSFX;

    void Start()
    {
        if (highlight == null)
            highlight = GetComponentInChildren<HintHighlight>();
        GameObject spawnedNumber = null;
        if (!isMine)
        {
            spawnedNumber = Instantiate(NumberPrefabs[adjacentMines], NumberSpawnPoint);
        }
        else
        {
            spawnedNumber = Instantiate(NumberPrefabs[6], NumberSpawnPoint);
        }

        if (spawnedNumber != null && isSup)
        {
            spawnedNumber.transform.Rotate(new Vector3(0, 0, 180));
            //spawnedNumber.transform.localPosition = new Vector3(spawnedNumber.transform.localPosition.x, -1, spawnedNumber.transform.localPosition.z);
        };

    }

    public void Reveal()
    {
        if (isRevealed)
            return;

        isRevealed = true;

        GameManager gm = FindFirstObjectByType<GameManager>();
        breakSFX.Play();
        InteractorHandedness? malletHand = null;

        malletHand = gm.getMalletHand();

        if (malletHand != null)
        {
            HapticsUtility.SendHapticImpulse(0.8f, 0.2f, malletHand.Equals(InteractorHandedness.Left) ? HapticsUtility.Controller.Left : HapticsUtility.Controller.Right);
        }

        if (isMine)
        {
            gm.LoseHealth();
        }
        else
        {
            gm.CheckWinCondition();
        }
    }


    void RevealAdjacent()
    {
        Collider[] nearby = Physics.OverlapSphere(transform.position, 1.1f); // Adjust size as needed

        foreach (Collider col in nearby)
        {
            Tooth t = col.GetComponent<Tooth>();
            if (t != null && !t.isRevealed)
            {
                t.Reveal();
            }
        }
    }
    public void SetFlag(GameObject flag)
    {
        if (flagSpawnPoint != null)
        {
            flag.transform.SetParent(flagSpawnPoint);
            flag.transform.localPosition = Vector3.zero;
            flag.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning("Flag spawn point not assigned on " + gameObject.name);
            flag.transform.position = transform.position;
        }

        currentFlag = flag;
    }

    public void RemoveFlag()
    {
        if (currentFlag != null)
        {
            currentFlag.transform.SetParent(null);
            currentFlag.SetActive(false);
            currentFlag = null;
        }
    }

    public void RemoveFragmentsOnDelay()
    {
        StartCoroutine(RemoveFragmentsCoroutine());
    }

    private IEnumerator RemoveFragmentsCoroutine()
    {
        yield return new WaitForSeconds(3f);
        Destroy(transform.GetChild(3).gameObject);
    }

    public bool HasFlag()
    {
        return currentFlag != null;
    }
}

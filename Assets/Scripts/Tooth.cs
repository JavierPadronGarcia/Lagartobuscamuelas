using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Tooth : MonoBehaviour
{

    public bool isMine = false;
    public bool isRevealed = false;
    public bool isSup = false;
    public int adjacentMines = 0;

    public Material defaultMaterial;
    public Material revealedMaterial;
    public Material mineMaterial;

    private Renderer rend;

    [SerializeField] Transform NumberSpawnPoint;
    [SerializeField] List<GameObject> NumberPrefabs;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend) rend.material = defaultMaterial;
        GameObject spawnedNumber = null;
        if (!isMine)
        {
            spawnedNumber = Instantiate(NumberPrefabs[adjacentMines], NumberSpawnPoint);
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

        if (isMine)
        {
            rend.material = mineMaterial;
            Debug.Log("Boom! You hit a mine.");
            // Trigger failure logic here
        }
        else
        {
            rend.material = revealedMaterial;
            Debug.Log("Revealed Tooth. Adjacent mines: " + adjacentMines);

            if (adjacentMines == 0)
            {
                // Optional: reveal nearby teeth automatically
                RevealAdjacent();
            }
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
}

using System.Collections.Generic;
using UnityEngine;

public class FlagPool : MonoBehaviour
{
    // Generamos la lista de objetos
    private List<GameObject> pooledObjects = new List<GameObject>();
    private List<GameObject> activeFlags = new List<GameObject>();
    [SerializeField] int amountToPool = 5;
    // Objeto que guardaremos en el pool
    [SerializeField] GameObject flagPrefab;
    [SerializeField] Material flagMaterial;

    // Método que se ejecuta antes del primer frame
    void Start()
    {
        // Generamos todos los objetos
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(flagPrefab, transform); // Instanciamos el objeto
            obj.SetActive(false); // Lo desactivamos

            // Cambio de material en el hijo "Plane" según el material asignado en el inspector
            Transform planeChild = obj.transform.Find("Plane");
            if (planeChild != null)
            {
                Renderer renderer = planeChild.GetComponent<Renderer>();
                if (renderer != null && flagMaterial != null)
                {
                    renderer.material = new Material(flagMaterial);
                }
                else
                {
                    Debug.LogWarning("Renderer or flagMaterial is missing on: " + obj.name);
                }
            }
            else
            {
                Debug.LogError("Plane child not found in prefab: " + obj.name);
            }
            pooledObjects.Add(obj); // Añadimos el objeto a la lista
        }
    }

    // --------------------------------------
    // MÉTODO GET POOLED OBJECT
    // --------------------------------------
    public GameObject GetPooledObject()
    {
        // Recorremos el pool
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // Si el objeto no está activo en la jerarquía quiere decir
            // que está disponible para su uso
            if (!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].SetActive(true);
                activeFlags.Add(pooledObjects[i]);
                return pooledObjects[i]; // Devolvemos el objeto disponible
            }
        }
        return null; // Si todos los objetos están ocupados devolvemos nulo
    }
    public void ReleaseLastActive()
    {
        if (activeFlags.Count > 0)
        {
            GameObject last = activeFlags[activeFlags.Count - 1];
            last.SetActive(false);
            activeFlags.RemoveAt(activeFlags.Count - 1);
        }
    }

    public void ReleaseAll()
    {
        foreach (GameObject obj in activeFlags)
        {
            obj.SetActive(false);
        }
        activeFlags.Clear();
    }
}

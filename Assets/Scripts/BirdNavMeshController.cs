using UnityEngine;
using UnityEngine.AI;

public class BirdNavMeshController : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float arrivalThreshold = 0.5f; // Qué tan cerca debe estar para considerar que llegó

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewDestination();
    }

    void Update()
    {
        // Si no está calculando ruta y ya está cerca del destino...
        if (!agent.pathPending && agent.remainingDistance <= arrivalThreshold)
        {
            SetNewDestination();
        }
    }

    void SetNewDestination()
    {
        Vector3 newPos = GetRandomNavMeshPosition(transform.position, wanderRadius);
        agent.SetDestination(newPos);
    }

    Vector3 GetRandomNavMeshPosition(Vector3 origin, float distance)
    {
        for (int i = 0; i < 30; i++) // evitar loop infinito
        {
            Vector3 randomDirection = Random.insideUnitSphere * distance;
            randomDirection += origin;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, distance, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return origin; // fallback si no encuentra posición
    }
}

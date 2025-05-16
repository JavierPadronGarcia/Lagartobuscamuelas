using UnityEngine;
public class LaserPointer : MonoBehaviour
{
    public float maxDistance = 100f;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, -transform.forward);
        lineRenderer.SetPosition(0, ray.origin);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, ray.origin + ray.direction * maxDistance);
        }
    }
}

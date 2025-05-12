using UnityEngine;

public class SetFlag : MonoBehaviour
{
    [SerializeField] private FlagPool yellowFlagPool;
    [SerializeField] private FlagPool redFlagPool;

    void Update()
    {
        // Z: Spawn Yellow Flag
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject flag = yellowFlagPool.GetPooledObject();
            if (flag != null)
            {
                flag.transform.position = new Vector3(Random.Range(-5, 5), 0, 0);
            }
        }

        // X: Disable last Yellow Flag
        if (Input.GetKeyDown(KeyCode.X))
        {
            yellowFlagPool.ReleaseLastActive();
        }

        // N: Spawn Red Flag
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameObject flag = redFlagPool.GetPooledObject();
            if (flag != null)
            {
                flag.transform.position = new Vector3(Random.Range(-5, 5), 0, 5);
            }
        }

        // M: Disable last Red Flag
        if (Input.GetKeyDown(KeyCode.M))
        {
            redFlagPool.ReleaseLastActive();
        }
    }
}

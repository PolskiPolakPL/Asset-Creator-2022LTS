using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] Vector3 spawnPosition;
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = spawnPosition;
    }
}

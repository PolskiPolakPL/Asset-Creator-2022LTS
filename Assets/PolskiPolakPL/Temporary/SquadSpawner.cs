using UnityEngine;

public class SquadSpawner : MonoBehaviour
{
    [SerializeField] KeyCode spawnKey = KeyCode.F;
    [SerializeField] GameObject squadPrefab;
    [SerializeField] Transform squadParent;
    [SerializeField] Transform marker;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnSquad();
        }
    }

    void SpawnSquad()
    {
        GameObject squadGO = Instantiate(squadPrefab,transform.position,transform.rotation,squadParent);
        squadGO.GetComponent<SquadScript>().Move(marker.position);
    }
}

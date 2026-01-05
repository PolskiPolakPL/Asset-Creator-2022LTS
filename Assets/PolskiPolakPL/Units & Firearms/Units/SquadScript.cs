using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadScript : MonoBehaviour
{
    [SerializeField] GameObject unitPrefab;
    [SerializeField] SquadData squadData;
    [SerializeField] int xOffset = 1;
    [SerializeField] int zOffset = 1;
    // Start is called before the first frame update
    void Start()
    {
        int xPos = 0, zPos = 0;
        GameObject unit;
        while(transform.childCount < squadData.SquadSize)
        {
            unit = Instantiate(unitPrefab, transform);
            unit.transform.localPosition = new Vector3 (xPos, 0, zPos);
            xPos += xOffset;
            zPos += zOffset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

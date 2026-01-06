using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadScript : MonoBehaviour
{
    [SerializeField] GameObject unitPrefab;
    [SerializeField] SquadData squadData;

    [SerializeField] float squadUnitsOffset = 1;
    int currentSquadSize = 0;

    private void Awake()
    {
        InitiateNewSquad(squadData.SquadSize, squadUnitsOffset);
    }

    void InitiateNewSquad(int numberOfUnits, float unitsOffset)
    {
        GameObject newUnit;
        //clearing children in transform
        foreach (Transform child in transform)
        {
            Debug.Log($"{child.name} was removed from {transform.name}");
            Destroy(child.gameObject);
        }
        //initializing new units
        for (int i = 0; i < numberOfUnits; i++)
        {
            newUnit = Instantiate(unitPrefab, transform);
            if (i > 0)
            {
                float angle = 2 * Mathf.PI * (i - 1) / (numberOfUnits - 1);
                float x = Mathf.Cos(angle) * unitsOffset;
                float z = Mathf.Sin(angle) * unitsOffset;
                newUnit.transform.localPosition = new Vector3(x, 0, z);
            }
            else
                newUnit.transform.localPosition = Vector3.zero;
        }
    }

}

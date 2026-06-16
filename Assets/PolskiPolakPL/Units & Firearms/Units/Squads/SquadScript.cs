using System.Collections.Generic;
using UnityEngine;


public class SquadScript : MonoBehaviour
{
    [SerializeField] UnitData unitData;
    [SerializeField] int maxSize;
    [SerializeField] FormationTypes formationType;
    [field: SerializeField] public List<UnitScript> Units { get; private set; } = new List<UnitScript>();
    float maxHealth;
    float currentHealth;

    private void Awake()
    {
        SpawnUnits();
    }

    public void SpawnUnits()
    {
        GameObject unitGO;
        for (int i = 0; i < maxSize; i++)
        {
            //unitGO = Instantiate(unitData.unitPrefab, transform);
            //AddUnit(unitGO.GetComponent<UnitScript>());
        }
        maxHealth = unitData.MaxHealth * maxSize;
        ArrangeUnits();
    }

    void ArrangeUnits(float unitDistance = 1)
    {
        int i = 0;
        List<Vector3> formationPos = SquadFormation.GetPositions(formationType, GetUnitCount(), unitDistance);
        //initializing new units
        foreach(var unit in Units)
        {
            unit.transform.localPosition = formationPos[i];
            i++;
        }
    }

    public void Move(Vector3 position)
    {
        int unitCount = GetUnitCount();
        List<Vector3> formationPos = SquadFormation.GetPositions(formationType, unitCount);

        for (int i = 0; i < unitCount; i++)
        {
            Units[i].Move(position + formationPos[i]);
        }
    }

    public void UpdateSquadHealth()
    {
        float health = 0;
        foreach(var unit in Units)
        {
            health += unit.health;
        }
        currentHealth = health;
    }

    public float GetSquadHealth()
    {
        UpdateSquadHealth();
        return currentHealth;
    }

    public UnitData GetUnitData()
    {
        return unitData;
    }

    public int GetUnitCount()
    {
        return Units.Count;
    }

    public void AddUnit(UnitScript unitScr)
    {
        if (GetUnitCount() >= maxSize)
            return;
        Units.Add(unitScr);
        unitScr.SetSquad(this);
    }

    public void RemoveUnit(UnitScript unitScr)
    {
        if (Units.Contains(unitScr))
            Units.Remove(unitScr);
        unitScr.SetSquad(null);
    }

}

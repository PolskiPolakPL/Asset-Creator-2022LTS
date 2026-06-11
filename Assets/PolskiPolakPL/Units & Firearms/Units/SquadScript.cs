using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SquadScript : MonoBehaviour
{
    [SerializeField] UnitData unitData;
    [SerializeField] int maxSize;
    [SerializeField] FormationTypes formationType;
    public UnitScript[] Units { get; private set; }
    int currentSquadSize = 0;
    float maxHealth;
    float currentHealth;

    private void Awake()
    {
        Units = GetComponentsInChildren<UnitScript>();
        maxHealth = unitData.MaxHealth * maxSize;
        Debug.Log($"Squad Size: {currentSquadSize} \t | \t Squad Health: {maxHealth}");
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

    public UnitData GetData()
    {
        return unitData;
    }

    public int GetUnitCount()
    {
        return Units.Length;
    }

    public bool AddUnit(UnitScript unitScr)
    {
        return false;
    }

    public bool RemoveUnit(UnitScript unitScr)
    {
        return false;
    }

}

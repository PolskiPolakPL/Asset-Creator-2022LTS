using UnityEngine;
using PolskiPolakPL.Utils;

public class RoleGiverScript : MonoBehaviour
{
    [SerializeField] ExampleEvent exampleScript;
    int cubeCount;
    int specialRoleCount = 3;
    int normalRoleCount;
    int poolSize;
    RandomTicket specialTicket;
    RandomTicket normalTicket;

    private void Start()
    {
        cubeCount = exampleScript.objectsToAppear.Count;
        normalRoleCount = cubeCount - specialRoleCount;
        specialTicket = new RandomTicket(specialRoleCount,0);
        normalTicket = new RandomTicket(normalRoleCount,0);
        Debug.Log($"Sum: {specialTicket.Weight} special tickets +{normalTicket.Weight} normal tickets = {cubeCount} cubes");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            DealTickets();
        }
    }

    void DealTickets()
    {
        Debug.Log("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
        int randomIndex;
        foreach (var cube in exampleScript.objectsToAppear)
        {
            poolSize = specialTicket.Weight + normalTicket.Weight;
            randomIndex = Random.Range(0, poolSize);
            randomIndex -=normalTicket.Weight;
            if(randomIndex < 0)
            {
                Debug.Log($"{cube.name} is normal");
                normalTicket.Subtract(1);
            }
            else
            {
                Debug.Log($"<color=#00ff00>{cube.name} IS SPECIAL!</color>");
                specialTicket.Subtract(1);
            }
        }

        specialTicket.SetWeight(specialRoleCount);
        normalTicket.SetWeight(normalRoleCount);
    }

}

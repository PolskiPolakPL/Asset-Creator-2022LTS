
using UnityEngine;
using System.Collections.Generic;

public class SquadMover : MonoBehaviour
{
    [SerializeField] SquadScript selectedSquad;
    [SerializeField] List<UnitScript> selectedUnits = new List<UnitScript>();

    Ray ray;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(!Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity))
                return;
            if (selectedSquad)
                selectedSquad.Move(hit.point);
            foreach(UnitScript unit in selectedUnits)
            {
                unit.Move(hit.point);
            }
        }
    }
}

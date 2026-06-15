
using UnityEngine;

public class SquadMover : MonoBehaviour
{
    [SerializeField] SquadScript selectedSquad;
    [SerializeField] UnitScript selectedUnit;

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
            if(selectedUnit)
                selectedUnit.Move(hit.point);
        }
    }
}

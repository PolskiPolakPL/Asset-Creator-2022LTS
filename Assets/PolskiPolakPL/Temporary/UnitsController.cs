
using UnityEngine;
using System.Collections.Generic;
using System.Drawing;

public class UnitsController : MonoBehaviour
{
    [SerializeField] List<UnitScript> selectedUnits = new List<UnitScript>();

    Vector3 startPosition;

    Ray ray;
    RaycastHit hit;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HandleLeftClick();
        }


        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!TryRaycastHit(out hit))
                return;
            startPosition = hit.point;
        }
        
        HandleRightClick();
    }

    bool TryRaycastHit(out RaycastHit hit)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity);
    }

    void HandleLeftClick()
    {
        if (!TryRaycastHit(out hit))
        {
            selectedUnits.Clear();
            return;
        }
        if (hit.collider.TryGetComponent<UnitScript>(out UnitScript unitScr))
            SelectUnit(unitScr);
        else
            MoveSelectedUnits(hit.point);
    }

    void HandleRightClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!TryRaycastHit(out hit))
                return;
            startPosition = hit.point;
            //FaceSelectedUnisAt(hit.point);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (!TryRaycastHit(out hit))
                return;
            Vector3 direction = hit.point - startPosition;
            FaceSelectedUnisTowards(direction);
        }
    }

    void FaceSelectedUnisAt(Vector3 point)
    {
        foreach (var unit in selectedUnits)
        {
            unit.aiMovement.SetAutoRotation(false);
            unit.aiMovement.LookAt(point);
        }
    }

    void FaceSelectedUnisTowards(Vector3 direction)
    {
        foreach (var unit in selectedUnits)
        {
            unit.aiMovement.SetAutoRotation(false);
            unit.aiMovement.Face(direction);
        }
    }

    void MoveSelectedUnits(Vector3 targetPosition)
    {
        foreach (UnitScript unit in selectedUnits)
        {
            unit.aiMovement.Move(targetPosition);
        }
    }

    void SelectUnit(UnitScript unitScr)
    {
        if (!Input.GetKey(KeyCode.LeftShift))
            selectedUnits.Clear();
        selectedUnits.Add(unitScr);
    }
}

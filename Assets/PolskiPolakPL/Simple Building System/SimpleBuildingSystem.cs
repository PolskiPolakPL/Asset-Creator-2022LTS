using System;
using UnityEngine;

public class SimpleBuildingSystem : MonoBehaviour
{
    //player camera
    [SerializeField] Transform playerCamT;
    [SerializeField] GameObject blockPrefab;
    [SerializeField] GameObject ghostPrefab;
    [SerializeField] LayerMask ignoreLayers = 4; //ignore raycast

    GameObject ghostGO;

    public float buildRange = 3;
    private Ray buildRay;

    private void Awake()
    {
        if (!playerCamT)
            playerCamT = Camera.main.transform;
    }


    private void Update()
    {
        if (ghostGO)
            UpdatePreviewPosition();
        else
            CreatePreview();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryPlaceBlock();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RemoveBlock();
        }

    }

    void UpdatePreviewPosition()
    {
        buildRay = new Ray(playerCamT.position, playerCamT.forward);
        if (!Physics.Raycast(buildRay, out RaycastHit hit, buildRange))
        {
            ghostGO.transform.position = playerCamT.position + playerCamT.forward * buildRange;
            return;
        }
        if(hit.collider.TryGetComponent<BuildingBlockScript>(out BuildingBlockScript blockScr))
        {
            Transform targetSnapPoint = blockScr.FindClosestSnapPoint(hit.point);
            ghostGO.transform.position = targetSnapPoint.position;
            ghostGO.transform.rotation = targetSnapPoint.localRotation;
            return;
        }
        ghostGO.transform.position = hit.point;
    }

    public void CreatePreview()
    {
        ghostGO = Instantiate(ghostPrefab, transform);
    }

    void DestroyPreview()
    {
        Destroy(ghostGO);
    }

    private void RemoveBlock()
    {
        buildRay = new Ray(playerCamT.position, playerCamT.forward);
        if (!Physics.Raycast(buildRay, out RaycastHit hit, buildRange))
            return;
        if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Block"))
            Destroy(hit.collider.gameObject);
    }

    private void TryPlaceBlock()
    {
        Transform ghostT = ghostGO.transform;
        Instantiate(blockPrefab, ghostT.position, ghostT.rotation);
    }
}

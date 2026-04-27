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

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!ghostGO)
                CreatePreview();
            else
                TryPlaceBlock();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(ghostGO)
                DestroyPreview();
            else
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
        if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            ghostGO.transform.position = hit.collider.transform.position + hit.normal * 0.5f;
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

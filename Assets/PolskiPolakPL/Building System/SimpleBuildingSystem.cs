using UnityEngine;

public class SimpleBuildingSystem : MonoBehaviour
{
    public static SimpleBuildingSystem Instance { get; private set; }

    //player camera
    [SerializeField] Transform playerCamT;
    public float buildRange = 3;
    private Ray buildRay;
    [SerializeField] LayerMask ignoreLayers;
    // preview
    [SerializeField] Material validMaterial;
    [SerializeField] Material invalidMaterial;
    private GameObject previewGO;
    public bool canPlace { get; private set; } = false;

    //structure
    public StructureSO structureData { get; private set; }

    private void Awake()
    {
        if (Instance && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
        if (!playerCamT)
            playerCamT = Camera.main.transform;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            TryRemoveStructure();
        }

        if (!structureData)
            return;

        if (!previewGO) // potential bug on swapping objects
            CreatePreview(structureData);

        UpdatePreviewPositionAndValidation();
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryPlaceStructure();
        }
    }

    void RotatePreview()
    {

    }

    bool TryPlaceStructure()
    {
        if(!canPlace)
            return false;
        PlaceStructure();
        return true;
    }

    void PlaceStructure()
    {
        Transform previewT = previewGO.transform;
        Instantiate(structureData.StructurePrefab, previewT.position, previewT.rotation);
        DestroyPreview();
    }

    void TryRemoveStructure()
    {
        if (!Physics.Raycast(buildRay, out RaycastHit hit, buildRange))
            return;
        if (!TryGetComponent<StructureScript>(out StructureScript structureScr))
            return;
        RemoveSctructure(structureScr);
    }

    void RemoveSctructure(StructureScript structureScr)
    {
        Destroy(structureScr.gameObject);
    }

    void UpdatePreviewPositionAndValidation()
    {
        bool didRaycastHit;
        if (!Physics.Raycast(buildRay, out RaycastHit hit, buildRange, ~ignoreLayers))
        {
            previewGO.transform.position = playerCamT.position + playerCamT.forward * buildRange;
            didRaycastHit = false;
        }
        else
        {
            previewGO.transform.position = hit.point;
            didRaycastHit = true;
        }
        UpdatePlacementValidation(didRaycastHit && IsPlacementValid());
    }

    public void CreatePreview(StructureSO structureData)
    {
        previewGO = Instantiate(structureData.GhostPrefab, transform);
        SetPreviewMaterial(canPlace);
    }

    void DestroyPreview()
    {
        if (previewGO)
            Destroy(previewGO);
        previewGO = null;
    }

    void SetPreviewMaterial(bool isValid)
    {
        Material material;
        if (isValid)
            material = validMaterial;
        else
            material = invalidMaterial;
        Renderer[] renderers = previewGO.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material = material;
        }
    }

    bool IsPreviewColliding()
    {
        Collider[] ownColliders = previewGO.GetComponentsInChildren<Collider>();
        foreach (var own in ownColliders)
        {
            Collider[] hits = Physics.OverlapBox(
                own.bounds.center,
                own.bounds.extents,
                own.transform.rotation,
                ~ignoreLayers //NOT 'Ignore Raycast' layer
            );

            foreach (var hit in hits)
            {
                if (!hit.GetComponent<StructureScript>())
                    continue;
                if (Physics.ComputePenetration(
                    own, own.transform.position, own.transform.rotation,
                    hit, hit.transform.position, hit.transform.rotation,
                    out Vector3 direction, out float distance))
                {
                    return true;
                }
            }
        }
        return false;
    }
    bool IsPlacementValid()
    {
        return !IsPreviewColliding();
    }
    void UpdatePlacementValidation(bool isValid)
    {
        if (isValid)
            AllowPlacement();
        else
            DenyPlacement();
    }
    void DenyPlacement()
    {
        if (!canPlace)
            return;
        canPlace = false;
        SetPreviewMaterial(false);
    }
    void AllowPlacement()
    {
        if (canPlace)
            return;
        canPlace = true;
        SetPreviewMaterial(true);
    }

}

using UnityEngine;

public class BuildingBlockScript : MonoBehaviour
{

    [field: SerializeField] public Transform[] snapPoints {  get; private set; }
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (snapPoints.Length <= 0)
            return;
        Gizmos.color = Color.blue;
        foreach(Transform snapPoint in snapPoints)
        {
            Gizmos.DrawCube(snapPoint.position, new Vector3(0.1f, 0.1f, 0.1f));
        }
    }

    public Transform FindClosestSnapPoint(Vector3 hitPoint)
    {
        Transform closestSnapPoint = snapPoints[0];
        float minDistance = Mathf.Infinity; // Distance between hit point and snap point
        for (int i = 0; i < snapPoints.Length; i++)
        {
            if(minDistance >= Vector3.Distance(hitPoint, snapPoints[i].position))
            {
                closestSnapPoint = snapPoints[i];
                minDistance = Vector3.Distance(hitPoint, snapPoints[i].position);
            }
        }
        return closestSnapPoint;
    }
}

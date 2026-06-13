using UnityEngine;
[CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObject/Unit Data")]
public class UnitData : ScriptableObject
{
    public GameObject unitPrefab;
    public float MaxHealth = 100;
    public float MovementSpeed = 3.2f;
    public float Sight = 20;
    public float ReactionTime = 1.1f;
}

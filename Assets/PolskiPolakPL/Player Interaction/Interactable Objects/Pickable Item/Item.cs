using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName = "ScriptableObject/Item")]
public class Item : ScriptableObject
{
    public GameObject PropPrafab;
    public GameObject ItemPrefab;
    public GameObject EquippedPrefab;
}

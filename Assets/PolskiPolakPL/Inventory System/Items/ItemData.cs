using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item",menuName = "ScriptableObject/ItemData")]
public class ItemData : ScriptableObject
{
    public string ID;
    public string DisplayName;
    public GameObject WorldPrefab;
    public GameObject HandPrefab;
    [Header("UI")]
    public Texture imageArray;
    public Rect UVRect = new Rect(0,0,0.1f,0.1f);
    [Header("Miscellaneous Stats")]
    public int StackSize;
    public int Durability;
}

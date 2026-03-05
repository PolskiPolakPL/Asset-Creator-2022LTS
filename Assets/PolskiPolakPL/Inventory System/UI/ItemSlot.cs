using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] ItemData heldItem;
    RawImage itemRawImage;
    private void Awake()
    {
        if (transform.childCount <= 0)
        {
            Debug.LogError($"{gameObject.name} does not have a child object!");
            return;
        }
        if(!transform.GetChild(0).TryGetComponent<RawImage>(out itemRawImage))
        {
            Debug.LogError($"{gameObject.name}'s child does not have RawImage component!");
            return;
        }
    }

    public ItemData GetItemData()
    {
        return heldItem;
    }

    public void SetItem(ItemData item, int arraySize)
    {
        heldItem = item;
        itemRawImage.uvRect = GetUVRectFromItemArray(arraySize);
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (HasItem())
        {
            itemRawImage.enabled = true;
        }
        else
        {
            itemRawImage.enabled = false;
        }
    }

    public void ClearSlot()
    {
        heldItem = null;
        UpdateSlot();
    }

    public bool HasItem()
    {
        return heldItem != null;
    }

    Rect GetUVRectFromItemArray(int itemArraySize)
    {
        float stepSize = 1f / itemArraySize;
        int rowIndex = heldItem.ID / itemArraySize;
        int columnIndex = heldItem.ID % itemArraySize;
        float x = stepSize * columnIndex;
        float y = stepSize * rowIndex;
        Debug.Log($"X: {x} \t Y: {y} \t stepSize: {stepSize}");
        return new Rect(x, y, stepSize, stepSize);
    }

}

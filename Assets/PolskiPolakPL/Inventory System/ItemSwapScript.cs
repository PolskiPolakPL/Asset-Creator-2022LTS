using UnityEngine;

public class ItemSwapScript : MonoBehaviour
{
    int selectedIndex = 0;
    int previousIndex;
    // Start is called before the first frame update
    void Start()
    {
        SetActiveHand(selectedIndex);
    }

    // Update is called once per frame
    void Update()
    {
        selectedIndex = InventoryScript.Instance.selectedIndex;
        if (selectedIndex != previousIndex)
        {
            SetActiveHand(selectedIndex);
            previousIndex = selectedIndex;
        }
        
            
    }

    void SetActiveHand(int selectedIndex)
    {
        int i = 0;
        foreach (Transform handSlot in transform)
        {
            if (selectedIndex == i)
            {
                handSlot.gameObject.SetActive(true);
            }
            else
                handSlot.gameObject.SetActive(false);
            i++;
        }
    }
}

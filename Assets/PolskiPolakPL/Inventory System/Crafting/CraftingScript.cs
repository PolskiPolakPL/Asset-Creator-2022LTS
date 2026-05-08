using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingScript : MonoBehaviour
{

    //CRAFTING
    [SerializeField] List<CraftingRecipe> allRecipes = new List<CraftingRecipe>();
    [SerializeField] Transform recipesContainer;
    [SerializeField] GameObject craftingBtnPrefab;
    [SerializeField] GameObject ingredientUIPrefab;

    InventorySystem inventorySystem;

    private void Start()
    {
        inventorySystem = InventorySystem.Instance;
        PopulateCraftingContainer();
    }

    //CRAFTING
    void PopulateCraftingContainer()
    {
        foreach (Transform child in recipesContainer)
            Destroy(child.gameObject);

        foreach (CraftingRecipe recipe in allRecipes)
        {
            GameObject btnGO = Instantiate(craftingBtnPrefab, recipesContainer);
            RawImage resultImage = btnGO.transform.GetChild(2).GetComponent<RawImage>();

            resultImage.texture = recipe.result.ImageTexture;
            resultImage.uvRect = recipe.result.UVRect;

            resultImage.gameObject.GetComponentInChildren<TMP_Text>().text = recipe.resultAmount.ToString();

            Button btn = btnGO.GetComponent<Button>();

            //btn.interactable = CanCraft(recipe);
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => Craft(recipe));

            foreach (CraftingIngredient ingredient in recipe.ingredients)
            {
                GameObject ingredientUI = Instantiate(ingredientUIPrefab, btnGO.transform.GetChild(0));
                RawImage ingredientImage = ingredientUI.GetComponent<RawImage>();
                ingredientImage.texture = ingredient.item.ImageTexture;
                ingredientImage.uvRect = ingredient.item.UVRect;
                ingredientUI.GetComponentInChildren<TMP_Text>().text = ingredient.amount.ToString();
            }
        }

    }
    void Craft(CraftingRecipe recipe)
    {
        if (!CanCraft(recipe))
            return;

        ConsumeIngredients(recipe);
        inventorySystem.AddItem(recipe.result, recipe.resultAmount);

        PopulateCraftingContainer();
    }
    bool CanCraft(CraftingRecipe recipe)
    {
        int totalFound;
        foreach (CraftingIngredient ingredient in recipe.ingredients)
        {
            totalFound = 0;
            foreach (ItemSlot slot in inventorySystem.playerInventorySlots)
            {
                if (!slot.HasItem() || slot.GetItem() != ingredient.item)
                    continue;

                totalFound += slot.GetAmount();
            }
            if (totalFound < ingredient.amount)
                return false;
        }
        return true;
    }
    void ConsumeIngredients(CraftingRecipe recipe)
    {
        int remaning, take;
        foreach (CraftingIngredient ingredient in recipe.ingredients)
        {
            remaning = ingredient.amount;

            foreach (ItemSlot slot in inventorySystem.playerInventorySlots)
            {
                if (!slot.HasItem()) continue;
                if (slot.GetItem() != ingredient.item) continue;

                take = Mathf.Min(slot.GetAmount(), remaning);
                slot.RemoveAmount(take);

                remaning -= take;
                if (remaning <= 0) break;
            }
        }
    }
}

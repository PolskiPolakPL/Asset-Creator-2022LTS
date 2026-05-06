using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Recipe", menuName = "ScriptableObject/Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<CraftingIngredient> ingredients;
    public ItemData result;
    public int resultAmount = 1;
}

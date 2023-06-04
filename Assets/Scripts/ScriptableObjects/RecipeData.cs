using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeData : ScriptableObject
{
    public List<KitchenObjectData> kitchenObjectDatas;
    public string recipeName;
}

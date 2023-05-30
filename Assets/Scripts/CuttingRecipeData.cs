using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeData : ScriptableObject {
    public KitchenObjectData input;
    public KitchenObjectData output;
    public int cuttingProgressMax;
    
}

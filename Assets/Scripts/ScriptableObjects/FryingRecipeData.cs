using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeData : ScriptableObject {
    public KitchenObjectData input;
    public KitchenObjectData output;
    public float fryingingProgressMax;
    
}

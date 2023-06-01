using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeData : ScriptableObject {
    public KitchenObjectData input;
    public KitchenObjectData output;
    public float burningingProgressMax;
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectData kitchenObjectData;
    }

    [SerializeField] private List<KitchenObjectData> validKitcheObjectDatas = new List<KitchenObjectData>();
    private List<KitchenObjectData> kitchenObjectDatas = new List<KitchenObjectData>();

    public bool TryAddIngredient(KitchenObjectData kitchenObjectData)
    {
        if(!validKitcheObjectDatas.Contains(kitchenObjectData))
        {
            return false;
        }
        if(kitchenObjectDatas.Contains(kitchenObjectData))
        {
            // Already has this type
            return false;
        }
        else
        {
            kitchenObjectDatas.Add(kitchenObjectData);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectData = kitchenObjectData
            });
            return true;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectData kitchenObjectData;
    
    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {
            if (!HasKitchenObject())
            {
                Transform kitchenObjectParent = GetKitchenObjectParentTransform();
                GameObject kitchenObjectTransform = Instantiate(kitchenObjectData.prefab, kitchenObjectParent.position, Quaternion.identity);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


}

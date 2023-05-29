using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectData cutKitchenObject;
    public override void Interact(Player player)
    {

        if(HasKitchenObject())
        {
            if(!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
        else
        {
            if(player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
    }

    public override void InteractAlternative(Player player)
    {
        if (HasKitchenObject())
        {
            GetKitchenObject().DestroySelf();
            GameObject kitchenObjectGameObject = Instantiate(cutKitchenObject.prefab);
            kitchenObjectGameObject.GetComponent<KitchenObject>().SetKitchenObjectParent(this);

        }
    }


}

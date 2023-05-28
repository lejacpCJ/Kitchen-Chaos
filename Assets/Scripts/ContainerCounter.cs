using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectData kitchenObjectData;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            GameObject kitchenObjectGameObject = Instantiate(kitchenObjectData.prefab);
            kitchenObjectGameObject.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }

}

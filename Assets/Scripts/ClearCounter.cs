using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectData kitchenObjectData;
    [SerializeField] private Transform topPosition;
    private KitchenObject kitchenObject;

    public void Interact(Player player)
    {
        if(kitchenObject == null)
        {
            if(player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                player.ClearKitchenObject();
            }
            else
            {
                GameObject kitchenObjectGameObject = Instantiate(kitchenObjectData.prefab, topPosition);
                kitchenObjectGameObject.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            }
        }
        else
        {
            kitchenObject.SetKitchenObjectParent(player);
        }
    }

    public Transform GetKitchenObjectParentTransform()
    {
        return topPosition.transform;
    }

    public void SetKitchebObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return this.kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return this.kitchenObject != null;
    }
}

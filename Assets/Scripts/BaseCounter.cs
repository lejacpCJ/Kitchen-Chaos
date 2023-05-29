using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform topPosition;
    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }
    public virtual void InteractAlternative(Player player)
    {
        Debug.LogError("BaseCounter.InteractAlternative();");
    }


    public Transform GetKitchenObjectParentTransform()
    {
        return topPosition;
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

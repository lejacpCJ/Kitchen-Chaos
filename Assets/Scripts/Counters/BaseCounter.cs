using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedOn;
    [SerializeField] private Transform topPosition;
    private KitchenObject kitchenObject;

    new public static void ResetStaticData()
    {
        OnAnyObjectPlacedOn = null;
    }
    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }
    public virtual void InteractAlternative(Player player)
    {
        //Debug.LogError("BaseCounter.InteractAlternative();");
    }


    public Transform GetKitchenObjectParentTransform()
    {
        return topPosition;
    }

    public void SetKitchebObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            OnAnyObjectPlacedOn?.Invoke(this, EventArgs.Empty);
        }
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

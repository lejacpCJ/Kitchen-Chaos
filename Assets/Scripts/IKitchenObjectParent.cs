using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectParentTransform();


    public void SetKitchebObject(KitchenObject kitchenObject);


    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();

}

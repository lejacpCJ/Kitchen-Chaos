using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitchenObjectData kitchenObjectData;
    private ClearCounter clearCounter;
    public KitchenObjectData GetKitchenObjectData()
    {
        return kitchenObjectData;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if(this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }
        this.clearCounter = clearCounter;
        clearCounter.SetKitchebObject(this);
        this.transform.parent = clearCounter.GetKitchenObjectParentTransform();
        this.transform.localPosition = Vector3.zero;
    }
    public ClearCounter GetClearCounter()
    {
        return this.clearCounter;
    }

}

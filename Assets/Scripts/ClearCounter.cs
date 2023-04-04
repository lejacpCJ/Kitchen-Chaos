using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectData kitchenObjectData;
    [SerializeField] private Transform topPosition;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;
    private KitchenObject kitchenObject;

    private void Update()
    {
        if(testing && Input.GetKeyDown(KeyCode.T))
        {
            if(kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
            }
        }
    }
    public void Interact()
    {
        if(kitchenObject == null)
        {
            GameObject kitchenObjectGameObject = Instantiate(kitchenObjectData.prefab, topPosition);
            kitchenObjectGameObject.GetComponent<KitchenObject>().SetClearCounter(this);
            kitchenObjectGameObject.transform.localPosition = Vector3.zero;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitchenObjectData kitchenObjectData;
    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectData GetKitchenObjectData()
    {
        return kitchenObjectData;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if(this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        kitchenObjectParent.SetKitchebObject(this);
        this.transform.parent = kitchenObjectParent.GetKitchenObjectParentTransform();
        this.transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return this.kitchenObjectParent;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static void SpawnKitchenObject(KitchenObjectData kitcheObjectData, IKitchenObjectParent parent)
    {
        GameObject kitchenObjectGameObject = Instantiate(kitcheObjectData.prefab);
        kitchenObjectGameObject.GetComponent<KitchenObject>().SetKitchenObjectParent(parent);
    }

}

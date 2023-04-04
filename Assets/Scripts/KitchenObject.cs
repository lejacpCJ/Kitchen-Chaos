using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitchenObjectData kitchenObjectData;
    public KitchenObjectData GetKitchenObjectData()
    {
        return kitchenObjectData;
    }

}

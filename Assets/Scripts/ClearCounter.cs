using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectData kitchenObjectData;
    [SerializeField] private Transform topPosition;
    public void Interact()
    {
        GameObject kitchenObject = Instantiate(kitchenObjectData.prefab, topPosition);
        kitchenObject.transform.localPosition = Vector3.zero;
    }
}

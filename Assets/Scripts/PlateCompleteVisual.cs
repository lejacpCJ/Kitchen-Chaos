using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable] public struct KitchenObjectData_GameObject
    {
        public KitchenObjectData kitchenObjectData;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectData_GameObject> kitchenObjectData_GameObjects = new List<KitchenObjectData_GameObject>();
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (var kitchenObjectData_GameObject in kitchenObjectData_GameObjects)
        {
            kitchenObjectData_GameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach(var kitchenObjectData_GameObject in kitchenObjectData_GameObjects)
        {
            if(kitchenObjectData_GameObject.kitchenObjectData == e.kitchenObjectData)
            {
                kitchenObjectData_GameObject.gameObject.SetActive(true);
            }
        }
    }
}

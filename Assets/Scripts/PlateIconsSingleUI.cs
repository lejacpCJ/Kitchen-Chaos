 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetKitchenObjectData(KitchenObjectData kitchenObjectData)
    {
        image.sprite = kitchenObjectData.sprite;
    }
}

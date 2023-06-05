using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeData(RecipeData recipeData)
    {
        recipeNameText.text = recipeData.recipeName;
    
        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectData kitchenObjectData in recipeData.kitchenObjectDatas)
        {
            Transform iconTranform = Instantiate(iconTemplate, iconContainer);
            iconTranform.gameObject.SetActive(true);
            iconTranform.GetComponent<Image>().sprite = kitchenObjectData.sprite;
        }
    }
}

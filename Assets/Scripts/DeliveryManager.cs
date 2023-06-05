using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListData recipeListData;
    private List<RecipeData> waitingRecipeDatas = new List<RecipeData>();
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;


    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeDatas.Count < waitingRecipesMax)
            {
                RecipeData waitingRecipeData = recipeListData.recipeDatas[UnityEngine.Random.Range(0, recipeListData.recipeDatas.Count)];
                waitingRecipeDatas.Add(waitingRecipeData);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        // Go through current waiting recipe
        for(int i = 0; i < waitingRecipeDatas.Count; i++)
        {
            RecipeData waitingRecipeData = waitingRecipeDatas[i];

            // Check the count of ingredient of waitingRecipeDatas[i] is equal to player's delivery
            if(waitingRecipeData.kitchenObjectDatas.Count == plateKitchenObject.GetKitchenObjectDatas().Count)
            {
                // Create a bool to check if all ingredients of player's delivery is equal to waitingRecipeDatas[i]
                bool plateContentsMatchRecipe = true;
                // Go through each ingredient in waitingRecipeDatas[i]
                foreach(KitchenObjectData recipeKitchenObjectData in waitingRecipeData.kitchenObjectDatas)
                {
                    bool ingredientFound = false;
                    foreach(KitchenObjectData plateKitchenObjectData in plateKitchenObject.GetKitchenObjectDatas())
                    {
                        if(plateKitchenObjectData == recipeKitchenObjectData)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if(!ingredientFound)
                    {
                        plateContentsMatchRecipe = false;
                    }
                }
                
                if(plateContentsMatchRecipe)
                {
                    waitingRecipeDatas.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        Debug.Log("Player delivered wrong recipe.");
    }

    public List<RecipeData> GetWaitingRecipeDatas()
    {
        return waitingRecipeDatas;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCut;
    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeData[] cuttingReceipeDatas;
    private int cuttingProgress;
    public override void Interact(Player player)
    {
        // There is an object on the table
        if(HasKitchenObject())
        {
            // Player do not have object holding.
            if(!player.HasKitchenObject())
            {
                // Player picks up the object.
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectData()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
        else
        {
            // Player holds an object
            if(player.HasKitchenObject())
            {
                // The object can be cut
                if(HasReceipeWithInput(player.GetKitchenObject().GetKitchenObjectData()))
                {
                    // Player put the object on the cutting counter.
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeData cuttingRecipeData = GetCuttingReceipeDataFromInput(GetKitchenObject().GetKitchenObjectData());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / (float)cuttingRecipeData.cuttingProgressMax
                    });
                }
            }
        }
    }

    public override void InteractAlternative(Player player)
    {
        // There is an object on the cutting counter and also the object can be cut.
        if (HasKitchenObject() && HasReceipeWithInput(GetKitchenObject().GetKitchenObjectData()))
        {
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeData cuttingRecipeData = GetCuttingReceipeDataFromInput(GetKitchenObject().GetKitchenObjectData());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / (float)cuttingRecipeData.cuttingProgressMax
            });
            int cuttingProgressMax = GetCuttingReceipeDataFromInput(GetKitchenObject().GetKitchenObjectData()).cuttingProgressMax;
            if(cuttingProgress >= cuttingProgressMax)
            {
                KitchenObjectData outputKitchenObjectData = GetOutputFromInput(GetKitchenObject().GetKitchenObjectData());
                GetKitchenObject().DestroySelf();
                GameObject kitchenObjectGameObject = Instantiate(outputKitchenObjectData.prefab);
                kitchenObjectGameObject.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            }

        }
    }

    private bool HasReceipeWithInput(KitchenObjectData inputKitchenObjectData)
    {
        CuttingRecipeData cuttingRecipeData = GetCuttingReceipeDataFromInput(inputKitchenObjectData);
        return cuttingRecipeData != null;
    }
    public KitchenObjectData GetOutputFromInput(KitchenObjectData inputKitchenObjectData)
    {
        CuttingRecipeData cuttingRecipeData = GetCuttingReceipeDataFromInput(inputKitchenObjectData);
        if(cuttingRecipeData != null)
        {
            return cuttingRecipeData.output;
        }
        return null;
    }

    private CuttingRecipeData GetCuttingReceipeDataFromInput(KitchenObjectData inputKitchenObjectData)
    {
        foreach (var cuttingRecipeData in cuttingReceipeDatas)
        {
            if (inputKitchenObjectData == cuttingRecipeData.input)
            {
                return cuttingRecipeData;
            }
        }
        return null;
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public FryingState fryingState;
    }
    public enum FryingState
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    public FryingState fryingState;
    [SerializeField] private FryingRecipeData[] fryingReceipeDatas;
    [SerializeField] private BurningRecipeData[] burningReceipeDatas;

    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeData fryingRecipeData;
    private BurningRecipeData burningRecipeData;



    private void Start()
    {
        fryingState = FryingState.Idle;
    }
    private void Update()
    {
        if(HasKitchenObject())
        {
            switch (fryingState)
            {
                case FryingState.Idle:
                    break;
                case FryingState.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeData.fryingingProgressMax
                    });

                    if (fryingTimer > fryingRecipeData.fryingingProgressMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeData.output, this);
                        fryingState = FryingState.Fried;
                        burningTimer = 0f;
                        burningRecipeData = GetBurningReceipeDataFromInput(GetKitchenObject().GetKitchenObjectData());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            fryingState = fryingState
                        }); 
                    }
                    break;
                case FryingState.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeData.burningingProgressMax
                    });

                    if (burningTimer > burningRecipeData.burningingProgressMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeData.output, this);
                        fryingState = FryingState.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            fryingState = fryingState
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });

                    }
                    break;
                case FryingState.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        // There is an object on the table
        if (HasKitchenObject())
        {
            // Player do not have object holding.
            if (!player.HasKitchenObject())
            {
                // Player picks up the object.
                GetKitchenObject().SetKitchenObjectParent(player);
                fryingState = FryingState.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    fryingState = fryingState
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
            else
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectData()))
                    {
                        GetKitchenObject().DestroySelf();

                        fryingState = FryingState.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            fryingState = fryingState
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });

                    }
                }
            }
        }
        else
        {
            // Player holds an object
            if (player.HasKitchenObject())
            {
                // The object can be fried
                if (HasReceipeWithInput(player.GetKitchenObject().GetKitchenObjectData()))
                {
                    // Player put the object on the cutting counter.
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeData = GetFryingReceipeDataFromInput(GetKitchenObject().GetKitchenObjectData());
                    fryingState = FryingState.Frying;
                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        fryingState = fryingState
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeData.fryingingProgressMax
                    });
                }
            }
        }
    }

    private bool HasReceipeWithInput(KitchenObjectData inputKitchenObjectData)
    {
        FryingRecipeData fryingRecipeData = GetFryingReceipeDataFromInput(inputKitchenObjectData);
        return fryingRecipeData != null;
    }
    public KitchenObjectData GetOutputFromInput(KitchenObjectData inputKitchenObjectData)
    {
        FryingRecipeData fryingRecipeData = GetFryingReceipeDataFromInput(inputKitchenObjectData);
        if (fryingRecipeData != null)
        {
            return fryingRecipeData.output;
        }
        return null;
    }

    private FryingRecipeData GetFryingReceipeDataFromInput(KitchenObjectData inputKitchenObjectData)
    {
        foreach (var fryingRecipeData in fryingReceipeDatas)
        {
            if (inputKitchenObjectData == fryingRecipeData.input)
            {
                return fryingRecipeData;
            }
        }
        return null;
    }

    private BurningRecipeData GetBurningReceipeDataFromInput(KitchenObjectData inputKitchenObjectData)
    {
        foreach (var burningRecipeData in burningReceipeDatas)
        {
            if (inputKitchenObjectData == burningRecipeData.input)
            {
                return burningRecipeData;
            }
        }
        return null;
    }


}

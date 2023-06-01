using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectData plateKitchenObjectData;


    private float spawnPlateTimer;
    private float spawnPlateTImerMax = 4f;
    private int plateSpawnedAmount;
    private int plateSpawnedAmountMax = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTImerMax)
        {
            if(plateSpawnedAmount < plateSpawnedAmountMax)
            {
                plateSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
            spawnPlateTimer = 0f;
        }
    }

    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {

        }
        else
        {
            if(plateSpawnedAmount > 0)
            {
                if(plateSpawnedAmount == 1)
                {
                    GetKitchenObject().DestroySelf();
                }
                plateSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectData, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);

            }
        }
    }
}

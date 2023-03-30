using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        Vector3 playerHeadPosition = transform.position + Vector3.up * playerHeight;
        bool canMove = !Physics.CapsuleCast(transform.position, playerHeadPosition, playerRadius, moveDir, moveDistance);
        
        if(!canMove)
        {
            // Cannot move towards moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, playerHeadPosition, playerRadius, moveDirX, moveDistance);

            if(canMove)
            {
                moveDir = moveDirX;
                
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, playerHeadPosition, playerRadius, moveDirZ, moveDistance);

                if(canMove)
                {
                    moveDir = moveDirZ;
                }

            }

        }

        if(canMove)
        {
            this.transform.position += moveDir * moveDistance;

        }
        
        isWalking = moveDir != Vector3.zero;
        this.transform.forward = Vector3.Slerp(this.transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}

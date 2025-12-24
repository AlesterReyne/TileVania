using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallJumping : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerMovement playerMovement;
    public bool isOnTheWall = false;

    public void OnJump(InputValue value)
    {
        Debug.Log("OnJump");
        if (value.isPressed && isOnTheWall)
        {
            playerMovement.OnJump(value);
            isOnTheWall = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollisionEnter2D");
        if (other.gameObject == player) isOnTheWall = true;
    }

    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     Debug.Log("OnCollisionExit2D");
    //     if (other.gameObject == player) isOnTheWall = false;
    // }
}
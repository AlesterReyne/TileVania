using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlueOrbJumping : MonoBehaviour
{
    [SerializeField] private GameObject player;

    //[SerializeField] private PlayerMovement playerMovement;
    //[SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private GameObject temporaryPlatform;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            // jumping
            //playerMovement.myRigidbody.linearVelocityY = 0;
            //playerMovement.myRigidbody.linearVelocity += new Vector2(0f, jumpSpeed);

            temporaryPlatform.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    private InputManagement inputManagement;
    private PlayerMovement playerMovement;

    private void Awake() 
    {
        inputManagement = GetComponent<InputManagement>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update() 
    {
        inputManagement.HandleAllInput();
    }

    private void FixedUpdate() 
    {
        playerMovement.HandleAllMovement();
    }
}

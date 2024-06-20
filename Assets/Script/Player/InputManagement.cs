using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagement : MonoBehaviour
{
    private PlayerInput playerInput;

    public Vector2 movementinput;
    public bool isRunning;
    public float verticalInput;
    public float horizontalInput;

    private void OnEnable() 
    {   
        if (playerInput == null)
        {
            playerInput = new PlayerInput();
            playerInput.Main.Movement.performed += i => movementinput = i.ReadValue<Vector2>();
            playerInput.Main.Run.started += _ => isRunning = true;
            playerInput.Main.Run.canceled += _ => isRunning = false;
        }

        playerInput.Enable();
    }

    private void OnDisable() 
    {
        playerInput.Disable();
    }

    public void HandleAllInput()
    {
        HandleMoveMentInput();
        // HandleJupmInput()
        // HandleJupInput()
    }
    private void HandleMoveMentInput()
    {
        verticalInput = movementinput.y;
        horizontalInput = movementinput.x;
    }
}

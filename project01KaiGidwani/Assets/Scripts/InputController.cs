using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    // The movemenet controller to update
    [SerializeField] private MovementController myMovmentController;

    //private Vector3 inputDirection;

    // The method that gets called to handle any player movement input
    public void OnMove(InputAction.CallbackContext context)
    {
        // Get the latest value for the input from the Input System
        //inputDirection = ;  // This is already normalized for us

        // Send that new direction to the Vehicle class
        myMovmentController.Direction = context.ReadValue<Vector2>();
    }
}

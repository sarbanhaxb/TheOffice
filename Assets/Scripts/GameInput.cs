using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    public static GameInput Instance { get; private set; }
    //public event EventHandler OnPlayerAction;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        //playerInputActions.Player.Interact.started += PlayerActionStart;
    }

    //private void PlayerActionStart(InputAction.CallbackContext context)
    //{
    //    OnPlayerAction?.Invoke(this, EventArgs.Empty);
    //}

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public Vector3 GetMousePosition() => Mouse.current.position.ReadValue();
}
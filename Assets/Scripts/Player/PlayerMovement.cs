using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    [SerializeField] private float moveSpeed = 10f;

    private Rigidbody2D rb;
    public GameInput GameInput { get; private set; }
    private bool isWalking;

    private void Awake()
    {
        Instance = this;
        GameInput = gameObject.AddComponent<GameInput>();
        rb = GetComponent<Rigidbody2D>();
        isWalking = false;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector().normalized;
        rb.MovePosition(rb.position + inputVector * (moveSpeed * Time.fixedDeltaTime));
        if (Mathf.Abs(inputVector.x) > 0 || Mathf.Abs(inputVector.y) > 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    public bool IsWalking() => isWalking;
    public Vector3 GetPlayerScreenPosition() => Camera.main.WorldToScreenPoint(transform.position);
}
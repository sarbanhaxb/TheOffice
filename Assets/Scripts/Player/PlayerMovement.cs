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

    private void Awake()
    {
        Instance = this;
        GameInput = gameObject.AddComponent<GameInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        PlayerStates ps = PlayerCurrentState.Instance.GetCurrentState();
        List<PlayerStates> psl = new() { PlayerStates.Present, PlayerStates.Working, PlayerStates.Smoking, PlayerStates.DrinkingWater };
        if (!psl.Contains(ps))
        {
            HandleMovement();
        }
        Debug.Log(PlayerCurrentState.Instance.GetCurrentState());
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector().normalized;
        rb.MovePosition(rb.position + inputVector * (moveSpeed * Time.fixedDeltaTime));
        if (Mathf.Abs(inputVector.x) > 0 || Mathf.Abs(inputVector.y) > 0)
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Walking);
        }
        else
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Idle);
        }
    }
    public Vector3 GetPlayerScreenPosition() => Camera.main.WorldToScreenPoint(transform.position);
}
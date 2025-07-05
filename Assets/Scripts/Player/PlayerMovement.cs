using System.Collections;
using System.Collections.Generic;
using TheOffice.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }
    public GameInput GameInput { get; private set; }

    [Header("��������� ��������")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float acceleration = 15f; // ��������� ��� �������
    [SerializeField] private float deceleration = 20f; // ���������� ��� ���������

    private Rigidbody2D _rb;
    private Vector2 _currentVelocity;
    private static readonly HashSet<PlayerStates> BlockedStates = new()
    {
        PlayerStates.Present,
        PlayerStates.Working,
        PlayerStates.Smoking,
        PlayerStates.DrinkingWater,
        PlayerStates.DrinkingCoffee,
        PlayerStates.Microwaving
    };

    private void Awake()
    {
        Instance = this;
        GameInput = gameObject.AddComponent<GameInput>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!BlockedStates.Contains(PlayerCurrentState.Instance.GetCurrentState()))
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        bool isMoving = inputVector.magnitude > 0.1f;

        // ������ ������� ��������
        Vector2 targetVelocity = inputVector.normalized * moveSpeed;

        // ������� ��������� ��������
        _currentVelocity = Vector2.Lerp(
            _currentVelocity,
            targetVelocity,
            (isMoving ? acceleration : deceleration) * Time.fixedDeltaTime
        );

        // ���������� ��������
        _rb.MovePosition(_rb.position + _currentVelocity * Time.fixedDeltaTime);

        // ���������� ���������
        PlayerCurrentState.Instance.SetState(
            isMoving ? PlayerStates.Walking : PlayerStates.Idle
        );
    }
    public Vector3 GetPlayerScreenPosition() => Camera.main.WorldToScreenPoint(transform.position);
}
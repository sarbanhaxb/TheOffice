using System.Collections;
using System.Collections.Generic;
using TheOffice.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }
    public GameInput GameInput { get; private set; }

    [Header("Настройки движения")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float acceleration = 15f; // Ускорение при разгоне
    [SerializeField] private float deceleration = 20f; // Замедление при остановке

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

        // Расчет целевой скорости
        Vector2 targetVelocity = inputVector.normalized * moveSpeed;

        // Плавное изменение скорости
        _currentVelocity = Vector2.Lerp(
            _currentVelocity,
            targetVelocity,
            (isMoving ? acceleration : deceleration) * Time.fixedDeltaTime
        );

        // Применение движения
        _rb.MovePosition(_rb.position + _currentVelocity * Time.fixedDeltaTime);

        // Обновление состояния
        PlayerCurrentState.Instance.SetState(
            isMoving ? PlayerStates.Walking : PlayerStates.Idle
        );
    }
    public Vector3 GetPlayerScreenPosition() => Camera.main.WorldToScreenPoint(transform.position);
}
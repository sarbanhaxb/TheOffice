using System;
using System.Collections;
using System.Collections.Generic;
using TheOffice.Utils;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVisual : MonoBehaviour
{
    [Header("Шкалы")]
    [SerializeField] private Image stressBar;
    [SerializeField] private Image starveBar;
    [SerializeField] private Image thirstBar;

    [Header("Финансы")]
    [SerializeField] private TMP_Text Money;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;

    private const string IS_WALKING = "IsWalking";
    private const string IS_SMOKING = "IsSmoking";
    private const string IS_PRESENT = "IsPresent";
    private const string IS_WORKING = "IsWorking";
    private const string IS_DRINKING = "IsDrinking";
    private const string IS_DRINKING_COFFEE = "IsDrinkingCoffee";
    private const string IS_MICROWAVING = "IsMicrowaving";
    private const string IS_TIRED = "IsTired";
    private const string IS_EATING = "IsEating";

    private static readonly Vector3 PRESENT_POSITION = new(40.5f, 2.5f, 0);
    private static readonly Vector3 WORKING_POSITION = new(9.3f, -1.7f, 0);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        UpdateStatsScale();
        UpdatePlayerDirection();
        UpdatePlayerState();
        UpdateFinanceState();
    }

    private void UpdateFinanceState()
    {
        Money.text = Math.Round(PlayerStats.Instance.currentMoney, 2).ToString();
    }
    private void UpdateStatsScale()
    {
        stressBar.fillAmount = PlayerStats.Instance._currentStressLevel / PlayerStats.Instance.maxStressLevel;
        starveBar.fillAmount = PlayerStats.Instance._currentStarveLevel / PlayerStats.Instance.maxStarveLevel;
        thirstBar.fillAmount = PlayerStats.Instance._currentThirstLevel/ PlayerStats.Instance.maxThirstLevel;
    }

    private void UpdatePlayerState()
    {
        var currentState = PlayerCurrentState.Instance.GetCurrentState();
        ResetAllBoolParameters();

        switch (currentState)
        {
            case PlayerStates.Walking:
                _animator.SetBool(IS_WALKING, true);
                break;
            case PlayerStates.Smoking:
                _animator.SetBool(IS_SMOKING, true);
                _spriteRenderer.flipX = true;
                break;
            case PlayerStates.Present:
                _animator.SetBool(IS_PRESENT, true);
                _rb.transform.position = PRESENT_POSITION;
                _spriteRenderer.flipX = true;
                break;
            case PlayerStates.Working:
                _animator.SetBool(IS_WORKING, true);
                _rb.transform.position = WORKING_POSITION;
                _spriteRenderer.flipX = false;
                break;
            case PlayerStates.DrinkingWater:
                _animator.SetBool(IS_DRINKING, true);
                _spriteRenderer.flipX = false;
                break;
            case PlayerStates.DrinkingCoffee:
                _animator.SetTrigger(IS_DRINKING_COFFEE);
                break;
            case PlayerStates.Tired:
                _animator.SetBool(IS_TIRED, true);
                break;
            case PlayerStates.Eating:
                _animator.SetBool(IS_EATING, true);
                break;
        }
    }

    private void ResetAllBoolParameters()
    {
        _animator.SetBool(IS_WALKING, false);
        _animator.SetBool(IS_SMOKING, false);
        _animator.SetBool(IS_PRESENT, false);
        _animator.SetBool(IS_WORKING, false);
        _animator.SetBool(IS_DRINKING, false);
        _animator.SetBool(IS_DRINKING_COFFEE, false);
        _animator.SetBool(IS_MICROWAVING, false);
        _animator.SetBool(IS_TIRED, false);
        _animator.SetBool(IS_EATING, false);
    }

    private void UpdatePlayerDirection()
    {
        var currentState = PlayerCurrentState.Instance.GetCurrentState();
        if (currentState == PlayerStates.Present) return;

        var mousePos = GameInput.Instance.GetMousePosition();
        var playerPos = PlayerMovement.Instance.GetPlayerScreenPosition();

        // Обновление направления взгляда
        if (currentState == PlayerStates.Walking || currentState == PlayerStates.Idle)
        {
            _spriteRenderer.flipX = mousePos.x >= playerPos.x;
        }

        // Обновление вертикального направления
        if (currentState == PlayerStates.Walking ||
            currentState == PlayerStates.Idle ||
            currentState == PlayerStates.Smoking ||
            currentState == PlayerStates.Talking)
        {
            _animator.SetFloat("MoveY", mousePos.y < playerPos.y ? -1 : 1);
        }
    }

    public void SetRandom(int count) => _animator.SetFloat("Random", UnityEngine.Random.Range(0, count));
    public void ResetState() => PlayerCurrentState.Instance.SetState(PlayerStates.Idle);
}
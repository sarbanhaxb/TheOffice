using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private const string IS_WALKING = "IsWalking";
    private const string IS_SMOKING = "IsSmoking";
    private const string IS_PRESENT = "IsPresent";
    private const string IS_WORKING = "IsWorking";
    private const string IS_DRINKING = "IsDrinking";


    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        GetAdjustPlayerDirection();
        CheckState();
    }

    private void CheckState()
    {
        switch (PlayerCurrentState.Instance.GetCurrentState())
        {
            case PlayerStates.Walking:
                animator.SetBool(IS_WALKING, true);
                break;
            case PlayerStates.Smoking:
                animator.SetBool(IS_SMOKING, true);
                GetComponent<SpriteRenderer>().flipX = true;
                break;
            case PlayerStates.Present:
                animator.SetBool(IS_PRESENT, true);
                GetComponentInParent<Rigidbody2D>().transform.position = new Vector3(40.5f, 2.5f, 0);
                GetComponent<SpriteRenderer>().flipX = true;
                break;
            case PlayerStates.Working:
                animator.SetBool(IS_WORKING, true);
                GetComponentInParent<Rigidbody2D>().transform.position = new Vector3(9.3f, -1.7f, 0);
                GetComponent<SpriteRenderer>().flipX = false;
                break;
            case PlayerStates.DrinkingWater:
                animator.SetBool(IS_DRINKING, true);
                GetComponent<SpriteRenderer>().flipX = false;
                break;
            case PlayerStates.Idle:
                animator.SetBool(IS_WALKING, false);
                animator.SetBool(IS_SMOKING, false);
                animator.SetBool(IS_PRESENT, false);
                animator.SetBool(IS_WORKING, false);
                animator.SetBool(IS_DRINKING, false);
                break;
        }
    }

    public void ResetState() => PlayerCurrentState.Instance.SetState(PlayerStates.Idle);

    private void GetAdjustPlayerDirection()
    {
        switch (PlayerCurrentState.Instance.GetCurrentState())
        {
            case PlayerStates.Idle:
                if (GameInput.Instance.GetMousePosition().x < PlayerMovement.Instance.GetPlayerScreenPosition().x)
                {
                    spriteRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                }
                if (GameInput.Instance.GetMousePosition().y < PlayerMovement.Instance.GetPlayerScreenPosition().y)
                {
                    animator.SetFloat("MoveY", -1);
                }
                else
                {
                    animator.SetFloat("MoveY", 1);
                }
                break;
            case PlayerStates.Smoking:
                if (GameInput.Instance.GetMousePosition().y < PlayerMovement.Instance.GetPlayerScreenPosition().y)
                {
                    animator.SetFloat("MoveY", -1);
                }
                else
                {
                    animator.SetFloat("MoveY", 1);
                }
                break;
            case PlayerStates.Talking:
                if (GameInput.Instance.GetMousePosition().y < PlayerMovement.Instance.GetPlayerScreenPosition().y)
                {
                    animator.SetFloat("MoveY", -1);
                }
                else
                {
                    animator.SetFloat("MoveY", 1);
                }
                break;
            case PlayerStates.Present:
                break;
            case PlayerStates.Walking:
                if (GameInput.Instance.GetMousePosition().x < PlayerMovement.Instance.GetPlayerScreenPosition().x)
                {
                    spriteRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                }

                if (GameInput.Instance.GetMousePosition().y < PlayerMovement.Instance.GetPlayerScreenPosition().y)
                {
                    animator.SetFloat("MoveY", -1);
                }
                else
                {
                    animator.SetFloat("MoveY", 1);
                }
                break;
        }
    }
}

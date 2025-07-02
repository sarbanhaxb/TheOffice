using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private const string IS_WALKING = "IsWalking";
    private const string IS_SMOKING = "IsSmoking";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        animator.SetBool(IS_WALKING, PlayerMovement.Instance.IsWalking());
        GetAdjustPlayerDirection();
    }


    private void GetAdjustPlayerDirection()
    {
        switch (PlayerCurrentState.Instanse.GetCurrentState())
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
                Debug.Log($"GetMousePosition: {GameInput.Instance.GetMousePosition().x}");
                Debug.Log($"GetPlayerScreenPosition: {PlayerMovement.Instance.GetPlayerScreenPosition().x}");
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
            case PlayerStates.Speaking:
                if (GameInput.Instance.GetMousePosition().y < PlayerMovement.Instance.GetPlayerScreenPosition().y)
                {
                    animator.SetFloat("MoveY", -1);
                }
                else
                {
                    animator.SetFloat("MoveY", 1);
                }
                break;
            case PlayerStates.Showing:

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

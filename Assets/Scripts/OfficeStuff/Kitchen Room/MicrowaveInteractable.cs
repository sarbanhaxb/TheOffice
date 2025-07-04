using System.Collections;
using System.Collections.Generic;
using TheOffice.Utils;
using UnityEngine;

public class MicrowaveInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    public GameObject microwafeZone;
    private Animator animator;
    private const string IS_MICROWAVING = "IsMicrowaving";

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void Interact()
    {
        if (PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.Microwaving)
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Microwaving);
            animator.SetTrigger(IS_MICROWAVING);
        }
        else
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Idle);
        }
    }

    public void ShowHint()
    {
        microwafeZone.SetActive(true);
    }
    public void HideHint()
    {
        microwafeZone.SetActive(false);
    }
}

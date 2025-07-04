using System.Collections;
using System.Collections.Generic;
using TheOffice.Utils;
using UnityEngine;
using UnityEngine.UI;

public class CoolerInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    public GameObject coolerZone;
    private Animator animator;
    private const string IS_POURING = "IsPouring";

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void Interact()
    {
        if (PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.DrinkingWater)
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.DrinkingWater);
            animator.SetTrigger(IS_POURING);
        }
        else
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Idle);
        }
    }

    public void ShowHint()
    {
        coolerZone.SetActive(true);
    }
    public void HideHint()
    {
        coolerZone.SetActive(false);
    }
}

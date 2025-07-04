using System.Collections;
using System.Collections.Generic;
using TheOffice.Utils;
using UnityEngine;

public class CoffeeInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    public GameObject coffeeZone;
    private Animator animator;
    private const string IS_COFFEE = "IsCoffee";

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void Interact()
    {
        if (PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.DrinkingCoffee)
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.DrinkingCoffee);
            animator.SetTrigger(IS_COFFEE);
        }
        else
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Idle);
        }
    }

    public void ShowHint()
    {
        coffeeZone.SetActive(true);
    }
    public void HideHint()
    {
        coffeeZone.SetActive(false);
    }
}

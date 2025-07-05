using System.Collections;
using System.Collections.Generic;
using TheOffice.Utils;
using UnityEngine;

public class CoffeeMagInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    public GameObject hintZone;

    [SerializeField] private float interactionPriority = 10f;


    public void Interact()
    {
        if (PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.DrinkingCoffee)
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.DrinkingCoffee);
            Destroy(gameObject);
        }
    }

    public void ShowHint()
    {
        hintZone.SetActive(true);
    }

    public void HideHint()
    {
        hintZone.SetActive(false);
    }

    public float GetPriority() => interactionPriority;

}

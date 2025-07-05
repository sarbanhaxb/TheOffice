using System.Collections;
using System.Collections.Generic;
using TheOffice.Utils;
using UnityEngine;

public class FoodInteractable : MonoBehaviour, IInteractable 
{
    [Header("—сылки")]
    public GameObject hintZone;

    [SerializeField] private float interactionPriority = 10f;


    public void Interact()
    {
        if (PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.Eating)
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Eating);
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

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


    [SerializeField] private float interactionPriority = 5f;

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
            coolerZone.GetComponent<Text>().text = "Press E to stop";
        }
        else
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Idle);
            coolerZone.GetComponent<Text>().text = "Press E to drink water";
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

    public float GetPriority() => interactionPriority;
}

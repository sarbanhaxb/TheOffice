using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentationInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    public GameObject presentHint;

    public void Interact()
    {
        if (PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.Present)
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Present);
            presentHint.GetComponent<Text>().text = "Press E to stop";
        }
        else
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Idle);
            presentHint.GetComponent<Text>().text = "Press E to present";
        }
    }

    public void ShowHint()
    {
        presentHint.SetActive(true);
    }
    public void HideHint()
    {
        presentHint.SetActive(false);
    }
}

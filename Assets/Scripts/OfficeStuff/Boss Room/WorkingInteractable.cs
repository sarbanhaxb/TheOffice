using System.Collections;
using System.Collections.Generic;
using TheOffice.Utils;
using UnityEngine;
using UnityEngine.UI;

public class WorkingInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    public GameObject workPlaceHint;

    public void Interact()
    {
        if (PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.Working)
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Working);
            workPlaceHint.GetComponent<Text>().text = "Press E to stop working";
        }
        else
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Idle);
            workPlaceHint.GetComponent<Text>().text = "Press E to start work";
        }
    }

    public void ShowHint()
    {
        workPlaceHint.SetActive(true);
    }
    public void HideHint()
    {
        workPlaceHint.SetActive(false);
    }
}

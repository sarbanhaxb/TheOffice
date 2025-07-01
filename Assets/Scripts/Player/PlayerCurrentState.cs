using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrentState : MonoBehaviour
{
    [SerializeField] PlayerStates currentState;
    public static PlayerCurrentState Instanse { get; private set; }

    private void Awake()
    {
        currentState = PlayerStates.Idle;
        Instanse = this;
    }

    private void FixedUpdate()
    {
        CheckCurrentState();
    }


    private void CheckCurrentState()
    {
        if (PlayerMovement.Instance.IsWalking())
        {
            currentState = PlayerStates.Walking;
        }
        else if (PlayerActions.Instance.IsSmoking())
        {
            currentState = PlayerStates.Smoking;
        }
        else if (PlayerActions.Instance.IsSpeaking())
        {
            currentState = PlayerStates.Speaking;
        }
        else
        {
            currentState = PlayerStates.Idle;
        }
    }

    public PlayerStates GetCurrentState() => currentState;
}

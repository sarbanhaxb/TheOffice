using System.Collections;
using System.Collections.Generic;
using TheOffice.Utils;
using UnityEngine;

public class PlayerCurrentState : MonoBehaviour
{
    [SerializeField] PlayerStates currentState;
    public static PlayerCurrentState Instance { get; private set; }

    private void Awake()
    {
        currentState = PlayerStates.Idle;
        Instance = this;
    }
    public void SetState(PlayerStates state) => currentState = state;
    public PlayerStates GetCurrentState() => currentState;
}
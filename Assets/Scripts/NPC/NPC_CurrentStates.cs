using System.Collections;
using System.Collections.Generic;
using TheOffice.Utils;
using UnityEngine;

public class NPC_CurrentStates : MonoBehaviour
{
    [SerializeField] NPCStates currentState;
    private void Awake()
    {
        currentState = NPCStates.Walking;
    }
    private void Update()
    {
        Debug.Log($"Current state: {currentState}");
    }
    public void SetState(NPCStates state) => currentState = state;
    public NPCStates GetCurrentState() => currentState;
}

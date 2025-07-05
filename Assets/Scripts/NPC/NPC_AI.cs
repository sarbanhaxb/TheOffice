using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TheOffice.Utils;

public class NPC_AI : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private StatesAI startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 2f;

    [Header("Интерактивные позиции")]
    [SerializeField] GameObject workPlace;
    [SerializeField] List<GameObject> coolerPlace;
    [SerializeField] GameObject smokeArea;

    private NavMeshAgent navMeshAgent;
    private StatesAI currentState;
    private float roamingTimer;
    private Vector3 roamingPosition;
    private Vector3 startingPosition;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = startingState;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        switch (currentState)
        {
            default:
            case StatesAI.Idle:
                break;
            case StatesAI.Walking:
                roamingTimer -= Time.deltaTime;
                if(roamingTimer < 0)
                {
                    Roaming();
                    roamingTimer = roamingTimerMax;
                }
                break;
        }
        Debug.Log(smokeArea.transform.position);
    }

    private void Roaming()
    {
        startingPosition = transform.position;
        roamingPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(roamingPosition);
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TheOffice.Utils;

public class PersonalAI : MonoBehaviour
{
    [SerializeField] private StatesAI startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    //[SerializeField] private float roamingTimerMax = 2f;

    private Vector3 currentDestination;

    private NavMeshAgent navMeshAgent;
    private StatesAI currentState;
    //private float roamingTimer;
    private Vector3 roamingPosition;
    private Vector3 startingPosition;
    List<Vector3> vector3s = new List<Vector3>();

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = startingState;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Start()
    {
        startingPosition = transform.position;

        vector3s.Add(new Vector3(-19.166193f, -11.966877f, 0f));
        vector3s.Add(new Vector3(-8.53369522f, 4.82197189f, 0));
        vector3s.Add(new Vector3(43f, 4f, 0));

        currentDestination = vector3s[UnityEngine.Random.Range(0, vector3s.Count)];
        navMeshAgent.SetDestination(currentDestination);

    }

    private void Update()
    {
        switch (currentState)
        {
            case StatesAI.Idle:
                break;
            case StatesAI.Walking:
                Roaming();
                break;
        }
    }

    private void Roaming()
    {
        //roamingPosition = GetRoamingPosition();
        if (transform.position == currentDestination)
        {
            currentDestination = vector3s[UnityEngine.Random.Range(0, vector3s.Count)];
            navMeshAgent.SetDestination(currentDestination);
        }
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    public StatesAI GetCurrentState() => currentState;
    public void SetCurrentState(StatesAI state) => currentState = state;

}
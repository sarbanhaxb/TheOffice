using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TheOffice.Utils;
using UnityEngine.UIElements;

public class NPC_Movement : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private NPCStates startingState;
    [SerializeField] private float stoppingDistanceFromPlayer = 5f;
    [SerializeField] private float destinationReachedThreshold = 0.1f; // Порог достижения цели

    [Header("Интерактивные позиции")]
    [SerializeField] GameObject workPlace;
    [SerializeField] List<GameObject> coolerPlace;
    [SerializeField] GameObject smokeArea;
    [SerializeField] GameObject microwaveArea;


    [Header("Цель")]
    [SerializeField] private GameObject target;

    private NavMeshAgent navMeshAgent;
    private float originalStoppingDistance;
    private NPC_Stats stats;
    private NPC_CurrentStates currentState;
    private bool isAtDestination = false;
    private GameObject currentDestination;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stats = GetComponent<NPC_Stats>();
        currentState = GetComponent<NPC_CurrentStates>();
        currentState.SetState(startingState);
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        originalStoppingDistance = navMeshAgent.stoppingDistance;
    }

    private void Update()
    {
        currentDestination = ChooseDestination();
        if (currentDestination != target)
        {
            isAtDestination = false;
            target = currentDestination;
        }
        navMeshAgent.SetDestination(target.transform.position);

        GameObject destination = ChooseDestination();
        navMeshAgent.SetDestination(destination.transform.position);
        

        // Проверяем, является ли цель игроком
        if (destination.CompareTag("Player"))
        {
            // Устанавливаем нужное расстояние остановки для игрока
            navMeshAgent.stoppingDistance = stoppingDistanceFromPlayer;
        }
        else
        {
            // Возвращаем стандартное расстояние остановки
            navMeshAgent.stoppingDistance = originalStoppingDistance;
        }
        // Проверяем, достиг ли NPC цели
        if (!isAtDestination && HasReachedDestination())
        {
            OnDestinationReached();
        }
    }

    private bool HasReachedDestination()
    {
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + destinationReachedThreshold)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDestinationReached()
    {
        isAtDestination = true;

        // Выполняем действия в зависимости от типа цели
        if (target == workPlace)
        {
            currentState.SetState(NPCStates.Working);
        }
        else if (coolerPlace.Contains(target))
        {
            // Пьем воду - уменьшаем жажду
        }
        else if (target == microwaveArea)
        {
            // Едим - уменьшаем голод
        }
        else if (target == smokeArea)
        {
            // Курим - уменьшаем стресс
            currentState.SetState(NPCStates.Smoking);
        }
    }

    private GameObject ChooseDestination()
    {
        if (stats.GetCurrentStressLevel() < 75f && stats.GetCurrentStarveLevel() < 75f && stats.GetCurrentThirstLevel() < 75f)
        {
            target = workPlace;
        }
        else if (stats.GetCurrentStarveLevel() > 75f)
        {
            target = microwaveArea;
        }
        else if (stats.GetCurrentThirstLevel() > 75f)
        {
            target = GetNearestCooler();
        }
        else if (stats.GetCurrentStressLevel() > 75f)
        {
            currentState.SetState(NPCStates.Walking);
            target = smokeArea;
        }
        return target;
    }

    private GameObject GetNearestCooler()
    {
        if (coolerPlace == null || coolerPlace.Count == 0) return workPlace;

        GameObject nearestCooler = null;
        float minDistance = float.MaxValue;

        foreach (GameObject cooler in coolerPlace)
        {
            if (cooler == null) continue;
            float distance = Vector3.Distance(transform.position, cooler.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestCooler = cooler;
            }
        }
        return nearestCooler != null ? nearestCooler : workPlace; // Если все кулеры null, возвращаем рабочее место
    }
}


//private float roamingTimer;
//private Vector3 roamingPosition;
//private Vector3 startingPosition;

//private void Roaming()
//{
//    startingPosition = transform.position;
//    roamingPosition = GetRoamingPosition();
//    navMeshAgent.SetDestination(roamingPosition);
//}

//private Vector3 GetRoamingPosition()
//{
//    return startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
//}

//private void Update()
//{
//    navMeshAgent.SetDestination(ChooseDestination());
//    //switch (currentState)
//    //{
//    //    default:
//    //    case StatesNPC.Idle:
//    //        break;
//    //    case StatesNPC.Walking:
//    //        roamingTimer -= Time.deltaTime;
//    //        if(roamingTimer < 0)
//    //        {
//    //            Roaming();
//    //            roamingTimer = roamingTimerMax;
//    //        }
//    //        break;
//    //}
//}
//[SerializeField] private float roamingDistanceMax = 7f;
//[SerializeField] private float roamingDistanceMin = 3f;
//[SerializeField] private float roamingTimerMax = 2f;
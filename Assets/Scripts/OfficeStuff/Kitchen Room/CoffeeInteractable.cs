using System;
using System.Collections;
using System.Collections.Generic;
using TheOffice.Utils;
using UnityEngine;

public class CoffeeInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    public GameObject coffeeZone;
    public GameObject coffeePrefab;
    public Vector3 coffeeSpawnPoint = new(70.65f, -11.52f, 0f);

    [Header("—сылки")]
    public float animationDuration = 5.8f;

    [SerializeField] private float interactionPriority = 5f;


    private Animator animator;
    private const string IS_COFFEE = "IsCoffee";
    private GameObject _currentCoffee;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void Interact()
    {
        if (PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.DrinkingCoffee && _currentCoffee == null)
        {
            StartCoroutine(MakeCoffeeRoutine());
        }
    }

    private IEnumerator MakeCoffeeRoutine()
    {
        animator.SetTrigger(IS_COFFEE);

        yield return new WaitForSeconds(animationDuration);

        if (_currentCoffee == null)
        {
            _currentCoffee = Instantiate(coffeePrefab, coffeeSpawnPoint, Quaternion.identity);
        }
    }

    public void ShowHint()
    {
        coffeeZone.SetActive(true);
    }
    public void HideHint()
    {
        coffeeZone.SetActive(false);
    }

    public float GetPriority() => interactionPriority;

}

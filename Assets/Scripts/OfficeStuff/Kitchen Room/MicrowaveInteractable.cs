using System.Collections;
using System.Collections.Generic;
using TheOffice.Utils;
using UnityEngine;

public class MicrowaveInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    public GameObject heatingZone;
    public GameObject foodPrefab;
    public Vector3 foodSpawnPoint = new(73.92f, -11.85f, 0f);

    [Header("—сылки")]
    public float animationDuration = 16.5f;

    [SerializeField] private float interactionPriority = 5f;


    private Animator animator;
    private const string IS_MICROWAVING = "IsMicrowaving";
    private GameObject _currentFood;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void Interact()
    {
        if (PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.Microwaving && _currentFood == null)
        {
            StartCoroutine(MakeFoodRoutine());
        }
    }

    private IEnumerator MakeFoodRoutine()
    {
        animator.SetTrigger(IS_MICROWAVING);

        yield return new WaitForSeconds(animationDuration);

        if (_currentFood == null)
        {
            _currentFood = Instantiate(foodPrefab, foodSpawnPoint, Quaternion.identity);
        }
    }

    public void ShowHint()
    {
        heatingZone.SetActive(true);
    }
    public void HideHint()
    {
        heatingZone.SetActive(false);
    }

    public float GetPriority() => interactionPriority;
}

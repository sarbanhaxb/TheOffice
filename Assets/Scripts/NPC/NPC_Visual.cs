using System.Collections.Generic;
using TheOffice.Utils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPC_Visual : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private NavMeshAgent _navMeshAgent;
    private NPC_Stats _stats;
    private NPC_CurrentStates _currentState;
    

    [Header("Шкалы")]
    [SerializeField] private Image stressBar;
    [SerializeField] private Image starveBar;
    [SerializeField] private Image thirstBar;

    // Параметры аниматора
    private const string IS_WALKING = "IsMoving";
    private const string IS_WORKING = "IsWorking";
    private const string MOVE_X = "MoveX";
    private const string MOVE_Y = "MoveY";

    [Header("Рабочее место")]
    [SerializeField] private GameObject table;

    private Vector2 lastMovementDirection;

    private void Awake()
    {
        _currentState = GetComponentInParent<NPC_CurrentStates>();
        _stats = GetComponentInParent<NPC_Stats>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    private void Update()
    {
        UpdatePlayerState();

        stressBar.transform.localScale = Vector3.one;

        stressBar.fillAmount = _stats.GetCurrentStressLevel() / _stats.GetMaxStressLevel();
        starveBar.fillAmount = _stats.GetCurrentStarveLevel() / _stats.GetMaxStarveLevel();
        thirstBar.fillAmount = _stats.GetCurrentThirstLevel() / _stats.GetMaxThirstLevel();

        Vector3 moveDirection = _navMeshAgent.velocity;
        bool isMoving = moveDirection.magnitude > 0.1f;

        _animator.SetBool(IS_WALKING, isMoving);

        if (isMoving)
        {
            // Запоминаем последнее направление движения
            lastMovementDirection = moveDirection.normalized;

            // Обновляем параметры анимации
            _animator.SetFloat(MOVE_X, Mathf.Abs(lastMovementDirection.x));
            _animator.SetFloat(MOVE_Y, lastMovementDirection.y);

            // Разворот спрайта по горизонтали
            _spriteRenderer.flipX = lastMovementDirection.x > 0.01f;
        }
        else
        {
            // При остановке сохраняем последнее направление
            // (параметры MOVE_X/MOVE_Y уже установлены)
        }
    }

    private void UpdatePlayerState()
    {
        var currentState = _currentState.GetCurrentState();

        switch(currentState)
        {
            case NPCStates.Working:
                _animator.SetBool(IS_WORKING, true);
                _spriteRenderer.flipX = false;
                var animator = table.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetBool(IS_WORKING, true); 
                    animator.SetFloat("CURRENTPOINT", _animator.GetFloat("Random"));
                }
                break;
        }

    }


    public void SetRandom(int count) => _animator.SetFloat("Random", UnityEngine.Random.Range(0, count));
    public void ResetState() => PlayerCurrentState.Instance.SetState(PlayerStates.Idle);

}
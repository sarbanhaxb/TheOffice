using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPC_Visual : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private NavMeshAgent navMeshAgent;
    private NPC_Stats stats;

    [Header("Шкалы")]
    [SerializeField] private Image stressBar;
    [SerializeField] private Image starveBar;
    [SerializeField] private Image thirstBar;

    // Параметры аниматора
    private const string IS_WALKING = "IsMoving";
    private const string MOVE_X = "MoveX";
    private const string MOVE_Y = "MoveY";

    private Vector2 lastMovementDirection;

    private void Awake()
    {
        stats = GetComponentInParent<NPC_Stats>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    private void Update()
    {
        stressBar.fillAmount = stats.GetCurrentStressLevel() / stats.GetMaxStressLevel();
        starveBar.fillAmount = stats.GetCurrentStarveLevel() / stats.GetMaxStarveLevel();
        thirstBar.fillAmount = stats.GetCurrentThirstLevel() / stats.GetMaxThirstLevel();

        Vector3 moveDirection = navMeshAgent.velocity;
        bool isMoving = moveDirection.magnitude > 0.1f;

        animator.SetBool(IS_WALKING, isMoving);

        if (isMoving)
        {
            // Запоминаем последнее направление движения
            lastMovementDirection = moveDirection.normalized;

            // Обновляем параметры анимации
            animator.SetFloat(MOVE_X, Mathf.Abs(lastMovementDirection.x));
            animator.SetFloat(MOVE_Y, lastMovementDirection.y);

            // Разворот спрайта по горизонтали
            spriteRenderer.flipX = lastMovementDirection.x > 0.01f;
        }
        else
        {
            // При остановке сохраняем последнее направление
            // (параметры MOVE_X/MOVE_Y уже установлены)
        }
    }
}
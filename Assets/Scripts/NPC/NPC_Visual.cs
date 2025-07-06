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

    private Color _originalColor;
    

    [Header("�����")]
    [SerializeField] private Image stressBar;
    [SerializeField] private Image starveBar;
    [SerializeField] private Image thirstBar;

    // ��������� ���������
    private const string IS_WALKING = "IsMoving";
    private const string IS_WORKING = "IsWorking";
    private const string IS_SMOKING = "IsSmoking";
    private const string MOVE_X = "MoveX";
    private const string MOVE_Y = "MoveY";

    [Header("������� �����")]
    [SerializeField] private GameObject table;

    private Vector2 lastMovementDirection;

    private void Awake()
    {
        _currentState = GetComponentInParent<NPC_CurrentStates>();
        _stats = GetComponentInParent<NPC_Stats>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _navMeshAgent = GetComponentInParent<NavMeshAgent>();

        _originalColor = _spriteRenderer.color;
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
            // ���������� ��������� ����������� ��������
            lastMovementDirection = moveDirection.normalized;

            // ��������� ��������� ��������
            _animator.SetFloat(MOVE_X, Mathf.Abs(lastMovementDirection.x));
            _animator.SetFloat(MOVE_Y, lastMovementDirection.y);

            // �������� ������� �� �����������
            _spriteRenderer.flipX = lastMovementDirection.x > 0.01f;
        }
        else
        {
            // ��� ��������� ��������� ��������� �����������
            // (��������� MOVE_X/MOVE_Y ��� �����������)
        }
    }

    public void HighLightObjectOn()
    {
        _spriteRenderer.color = Color.green;
    }

    public void HighLightObjectOff()
    {
        _spriteRenderer.color = _originalColor;
    }

    private void UpdatePlayerState()
    {
        var currentState = _currentState.GetCurrentState();
        ResetAllBoolParameters();

        switch (currentState)
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
            case NPCStates.Walking:
                _animator.SetBool(IS_WALKING, true);
                break;
            case NPCStates.Smoking:
                _animator.SetBool(IS_SMOKING, true);
                break;
        }
    }

    private void ResetAllBoolParameters()
    {
        _animator.SetBool(IS_WALKING, false);
        _animator.SetBool(IS_SMOKING, false);
        _animator.SetBool(IS_WORKING, false);
    }


    public void SetRandom(int count) => _animator.SetFloat("Random", UnityEngine.Random.Range(0, count));
    public void ResetState() => PlayerCurrentState.Instance.SetState(PlayerStates.Idle);

}
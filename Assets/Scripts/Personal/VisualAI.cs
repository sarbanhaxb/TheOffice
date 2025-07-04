using System.Collections;
using System.Collections.Generic;
using TheOffice.Utils;
using UnityEngine;

public class VisualAI : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PersonalAI personalAI;
    private StatesAI currentState;
    private Vector3 currentPosition;

    private const string IS_SMOKING = "IsSmoking";
    private const string IS_MOVING = "IsMoving";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        personalAI = GetComponentInParent<PersonalAI>();
    }

    private void Start()
    {
        currentPosition = transform.position;
    }

    private void FixedUpdate()
    {
        AnimationController();
    }

    private void AnimationController()
    {
        switch (currentState)
        {
            case StatesAI.Walking:
                animator.SetBool(IS_MOVING, true); 
                break;
            case StatesAI.Idle:
                animator.SetBool(IS_MOVING, false); 
                break;
        }
    }
}

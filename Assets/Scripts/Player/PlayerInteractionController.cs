using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionController : MonoBehaviour
{
    [Header("Настройки")]
    public float interactionRange = 1.5f;
    public LayerMask interactableLayer;

    private IInteractable _currentInteractable;
    private Vector2 _facingDirection = Vector2.right;

    private void Update()
    {
        FindInteractable();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && _currentInteractable != null)
        {
            _currentInteractable.Interact();
        }
    }



    private void FindInteractable()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);

        Vector3 vector = transform.position;
        vector.y += 3f;

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IInteractable interactable))
            {
                _currentInteractable = interactable;
                interactable.ShowHint();
                return;
            }
        }

        if (_currentInteractable != null)
        {
            _currentInteractable.HideHint();
            _currentInteractable = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 vector = transform.position;
        vector.y += 3f;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(vector, interactionRange);
    }
}
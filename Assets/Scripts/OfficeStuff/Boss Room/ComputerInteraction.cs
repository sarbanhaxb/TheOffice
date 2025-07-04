using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerInteraction : MonoBehaviour, IInteractable
{
    [Header("Настройки")]
    public GameObject desktopScreen;
    public Transform playerSeatPosition;
    public InputActionReference toggleAction;

    [Header("Ссылки")]
    public Animator playerAnimator;
    private PlayerInteractionController _controls;
    private bool _isPlayerUsing = false;

    private void Awake()
    {
        _controls = new PlayerInteractionController();
        toggleAction.action.performed += _ => ToggleDesktop();
    }

    public void Interact()
    {
        if (!_isPlayerUsing)
        {
            _isPlayerUsing = true;
        }
    }


    private void ToggleDesktop()
    {
        if (!_isPlayerUsing) return;
        
        desktopScreen.SetActive(!desktopScreen.activeSelf);
    }


    public void ShowHint()
    {
        throw new System.NotImplementedException();
    }

    public void HideHint()
    {
        throw new System.NotImplementedException();
    }
}

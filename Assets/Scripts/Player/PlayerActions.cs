using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public static PlayerActions Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //GameInput.Instance.OnPlayerAction += OnAction;
    }

    private void OnAction(object sender, System.EventArgs e)
    {
        
    }
}

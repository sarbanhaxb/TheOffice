using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private bool isSmoking;
    private bool isSpeaking;
    private bool isShowing;

    public static PlayerActions Instance;

    private void Awake()
    {
        Instance = this;
        isSmoking = false;
        isSpeaking = false;
        isShowing = false;
    }

    private void Smoking()
    {
        isSmoking = true;
    }

    private void Speaking()
    {
        isSpeaking = true;
    }

    private void Showing()
    {
        isShowing = true;
    }

    public bool IsSmoking() => isSmoking;
    public bool IsSpeaking() => isSpeaking;
    public bool IsShowing() => isShowing;

}

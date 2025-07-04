using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheOffice.Utils
{
    public static class Utils
    {
        public static Vector3 GetRandomDir() => new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;


    }
    public enum StatesAI
    {
        Idle,
        Walking
    }

    public enum PlayerStates
    {
        Idle,
        Walking,
        Smoking,
        Talking,
        Present,
        Working,
        DrinkingWater,
        DrinkingCoffee,
        Microwaving
    }
}

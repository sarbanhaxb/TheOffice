using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Состояние персонажа")]
    [SerializeField] internal float maxStressLevel = 100f;
    [SerializeField] internal float stressDecreaseRate = 0.5f;
    [SerializeField] internal float maxStarveLevel = 100f;
    [SerializeField] internal float hungerIncreaseRate = 0.3f;
    [SerializeField] internal float maxThirstLevel = 100f;
    [SerializeField] internal float thirstIncreaseRate = 0.4f;

    internal float _currentStressLevel;
    internal float _currentStarveLevel;
    internal float _currentThirstLevel;

    public static PlayerStats Instance;

    private void Awake()
    {
        Instance = this;
        _currentStressLevel = maxStressLevel;
        _currentStarveLevel = 0f; // Начинаем с пустого уровня
        _currentThirstLevel = 0f;
    }
    private void Update()
    {
        // Плавное изменение показателей с учетом дельты времени
        UpdateStressLevel();
        UpdateHungerLevel();
        UpdateThirstLevel();
    }

    private void UpdateStressLevel()
    {
        _currentStressLevel = Mathf.MoveTowards(
            _currentStressLevel,
            0f,
            stressDecreaseRate * Time.deltaTime
        );
    }

    private void UpdateHungerLevel()
    {
        _currentStarveLevel = Mathf.MoveTowards(
            _currentStarveLevel,
            maxStarveLevel,
            hungerIncreaseRate * Time.deltaTime
        );
    }

    private void UpdateThirstLevel()
    {
        _currentThirstLevel = Mathf.MoveTowards(
            _currentThirstLevel,
            maxThirstLevel,
            thirstIncreaseRate * Time.deltaTime
        );
    }
}

using TheOffice.Utils;
using UnityEngine;

public class NPC_Stats : MonoBehaviour
{
    [Header("Состояние рабочего")]
    [SerializeField] private float maxStressLevel = 100f;
    [SerializeField] private float stressIncreaseRate;
    [SerializeField] private float defaultStressIncreaseRate = 0.2f;
    [SerializeField] private float maxStarveLevel = 100f;
    [SerializeField] private float hungerIncreaseRate;
    [SerializeField] private float defaultHungerIncreaseRate = 0.3f;
    [SerializeField] private float maxThirstLevel = 100f;
    [SerializeField] private float thirstIncreaseRate;
    [SerializeField] private float defaultThirstIncreaseRate = 0.4f;

    [Header("Характеристики рабочего")]
    [SerializeField] private float workingSpeed = 1f;

    private float _currentStressLevel;
    private float _currentStarveLevel;
    private float _currentThirstLevel;

    private NPC_CurrentStates _currentState;

    private void Awake()
    {
        ResetAllIncreaseRate();
        _currentStressLevel = 0f;
        _currentStarveLevel = 0f;
        _currentThirstLevel = 0f;
        _currentState = GetComponent<NPC_CurrentStates>();
    }

    private void Update()
    {
        UpdateStressLevel();
        UpdateHungerLevel();
        UpdateThirstLevel();
        CheckCurrentState();
    }

    private void UpdateStressLevel()
    {
        _currentStressLevel = Mathf.Clamp(
            _currentStressLevel + stressIncreaseRate * Time.deltaTime,
            0f, maxStressLevel
        );
    }

    private void UpdateHungerLevel()
    {
        _currentStarveLevel = Mathf.Clamp(
            _currentStarveLevel + hungerIncreaseRate * Time.deltaTime,
            0f,
            maxStarveLevel
        );
    }

    private void UpdateThirstLevel()
    {
        _currentThirstLevel = Mathf.Clamp(
            _currentThirstLevel + thirstIncreaseRate * Time.deltaTime,
            0f,
            maxThirstLevel
        );
    }

    private void CheckCurrentState()
    {
        switch (_currentState.GetCurrentState())
        {
            case NPCStates.Working:
                stressIncreaseRate = 2f;
                break;
            case NPCStates.Smoking:
                stressIncreaseRate = -5f;
                break;
            case NPCStates.DrinkWater:
                break;
            case NPCStates.DrinkCoffee:
                break;
            default:
                ResetAllIncreaseRate();
                break;
        }
    }


    private void ResetAllIncreaseRate()
    {
        stressIncreaseRate = defaultStressIncreaseRate;
        hungerIncreaseRate = defaultHungerIncreaseRate;
        thirstIncreaseRate = defaultThirstIncreaseRate;
    }

    public float GetMaxStressLevel() => maxStressLevel;
    public float GetCurrentStressLevel() => _currentStressLevel;
    public float GetMaxStarveLevel() => maxStarveLevel;
    public float GetCurrentStarveLevel() => _currentStarveLevel;
    public float GetMaxThirstLevel() => maxThirstLevel;
    public float GetCurrentThirstLevel() => _currentThirstLevel;
}
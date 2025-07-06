using TheOffice.Utils;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Состояние персонажа")]
    [SerializeField] internal float maxStressLevel = 100f;
    [SerializeField] internal float stressIncreaseRate; // Базовая скорость роста стресса
    [SerializeField] private float defaultStressIncreaseRate = 0.2f;
    [SerializeField] internal float maxStarveLevel = 100f;
    [SerializeField] internal float hungerIncreaseRate;
    [SerializeField] private float defaultHungerIncreaseRate = 0.3f;
    [SerializeField] internal float maxThirstLevel = 100f;
    [SerializeField] internal float thirstIncreaseRate;
    [SerializeField] private float defaultThirstIncreaseRate = 0.4f;

    [Header("Настройки скорости")]
    [SerializeField] private float baseMoveSpeed = 10f;
    [SerializeField] private float minMoveSpeed = 2f;
    [SerializeField] private float stressSpeedMultiplier = 0.5f;

    [Header("Финансы")]
    [SerializeField] internal float currentMoney;
    [SerializeField] private float baseMoneyPerSecond = 1f;
    [SerializeField] private float maxMoneyMultiplier = 2f;

    [Header("Эффект кофе")]
    [SerializeField] private float coffeeSpeedBoost = 3f; // Множитель скорости
    [SerializeField] private float coffeeEffectDuration = 15f; // Длительность в секундах
    [SerializeField] private Image coffeeTimerUI;
    private float _coffeeEffectTimer = 0f;
    private bool _hasCoffeeEffect = false;

    internal float _currentMoveSpeed;
    internal float _currentStressLevel;
    internal float _currentStarveLevel;
    internal float _currentThirstLevel;

    public static PlayerStats Instance;

    private void Awake()
    {
        Instance = this;
        _currentStressLevel = 0f; // Начинаем с нулевого стресса
        _currentStarveLevel = 0f;
        _currentThirstLevel = 0f;
        _currentMoveSpeed = baseMoveSpeed;
        currentMoney = 0;

        stressIncreaseRate = defaultStressIncreaseRate;
        hungerIncreaseRate = defaultHungerIncreaseRate;
        thirstIncreaseRate = defaultThirstIncreaseRate;
    }

    private void Update()
    {
        CheckCurrentState();
        UpdateStressLevel();
        UpdateHungerLevel();
        UpdateThirstLevel();
        UpdateMoveSpeed();
        UpdateFinancialStatus();
        UpdateCoffeeEffect();

        //Debug.Log($"Current stress: {_currentStressLevel}");
        //Debug.Log($"Current thirst: {_currentThirstLevel}");
        //Debug.Log($"Current starve: {_currentStarveLevel}");
    }

    private void UpdateCoffeeEffect()
    {
        if (_hasCoffeeEffect)
        {
            _coffeeEffectTimer -= Time.deltaTime;
            coffeeTimerUI.fillAmount = _coffeeEffectTimer / coffeeEffectDuration;
            if (_coffeeEffectTimer <= 0f)
            {
                _hasCoffeeEffect = false;
                coffeeTimerUI.gameObject.SetActive(false);
            }
        }
    }
    public void ApplyCoffeeEffect()
    {
        _hasCoffeeEffect = true;
        _coffeeEffectTimer = coffeeEffectDuration;
        coffeeTimerUI.gameObject.SetActive(true);
    }

    private void UpdateFinancialStatus()
    {
        if (PlayerCurrentState.Instance.GetCurrentState() == PlayerStates.Working)
        {
            float stressFactor = 1 - (_currentStressLevel / maxStressLevel);
            float moneyEarned = baseMoneyPerSecond * stressFactor * Time.deltaTime;
            currentMoney += moneyEarned;
        }
    }

    private void UpdateMoveSpeed()
    {
        float stressImpact = _currentStressLevel / maxStressLevel;
        float targetSpeed = Mathf.Lerp(baseMoveSpeed, minMoveSpeed, stressImpact * stressSpeedMultiplier);

        if (_hasCoffeeEffect)
        {
            float boostProgress = _coffeeEffectTimer / coffeeEffectDuration;
            float currentBoost = 1 + (coffeeSpeedBoost - 1) * boostProgress;
            _currentMoveSpeed = targetSpeed * currentBoost;
            baseMoneyPerSecond = 5f;
        }
        else
        {
            _currentMoveSpeed = targetSpeed;
            baseMoneyPerSecond = 1f;
        }
    }

    // Проверка текущего состояния и изменение модификаторов характеристик
    private void CheckCurrentState()
    {
        switch (PlayerCurrentState.Instance.GetCurrentState())
        {
            case PlayerStates.Smoking:
                stressIncreaseRate = -5f;
                break;
            case PlayerStates.Working:
                stressIncreaseRate = 2f;
                break;
            case PlayerStates.DrinkingWater:
                stressIncreaseRate = -1.5f;
                thirstIncreaseRate = -5f;
                break;
            case PlayerStates.Present:
                stressIncreaseRate = 1.5f;
                break;
            case PlayerStates.DrinkingCoffee:
                stressIncreaseRate = -4f;
                thirstIncreaseRate = -1.5f;
                ApplyCoffeeEffect();
                break;
            case PlayerStates.Eating:
                stressIncreaseRate = -3f;
                hungerIncreaseRate = -10f;
                break;
            default:
                stressIncreaseRate = defaultStressIncreaseRate; // Возвращает значения к изначальным
                thirstIncreaseRate = defaultThirstIncreaseRate;
                hungerIncreaseRate = defaultHungerIncreaseRate;
                break;
        }
    }

    private void UpdateStressLevel()
    {
        _currentStressLevel = Mathf.Clamp(
            _currentStressLevel + stressIncreaseRate * Time.deltaTime,
            0f,
            maxStressLevel
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
}
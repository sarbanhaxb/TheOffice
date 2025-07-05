using UnityEngine;

public class NPC_Stats : MonoBehaviour
{
    [Header("��������� ��������")]
    [SerializeField] private float maxStressLevel = 100f;
    [SerializeField] private float stressDecreaseRate = 0.5f;
    [SerializeField] private float maxStarveLevel = 100f;
    [SerializeField] private float hungerIncreaseRate = 0.3f;
    [SerializeField] private float maxThirstLevel = 100f;
    [SerializeField] private float thirstIncreaseRate = 0.4f;

    [Header("�������������� ��������")]
    [SerializeField] private float workingSpeed = 1f;

    private float _currentStressLevel;
    private float _currentStarveLevel;
    private float _currentThirstLevel;

    private void Awake()
    {
        _currentStressLevel = 0f;
        _currentStarveLevel = 0f; // �������� � ������� ������
        _currentThirstLevel = 0f;
    }

    private void Update()
    {
        // ������� ��������� ����������� � ������ ������ �������
        UpdateStressLevel();
        UpdateHungerLevel();
        UpdateThirstLevel();
    }

    private void UpdateStressLevel()
    {
        _currentStressLevel = Mathf.MoveTowards(
            _currentStressLevel,
            maxStressLevel,
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

    // ������ ��� �������������� �����������
    public void ReduceStress(float amount)
    {
        _currentStressLevel = Mathf.Clamp(_currentStressLevel - amount, 0f, maxStressLevel);
    }

    public void Eat(float amount)
    {
        _currentStarveLevel = Mathf.Clamp(_currentStarveLevel - amount, 0f, maxStarveLevel);
    }

    public void Drink(float amount)
    {
        _currentThirstLevel = Mathf.Clamp(_currentThirstLevel - amount, 0f, maxThirstLevel);
    }

    // �������
    public float GetCurrentStressLevel() => _currentStressLevel;
    public float GetMaxStressLevel() => maxStressLevel;
    public float GetCurrentStarveLevel() => _currentStarveLevel;
    public float GetMaxStarveLevel() => maxStarveLevel;
    public float GetCurrentThirstLevel() => _currentThirstLevel;
    public float GetMaxThirstLevel() => maxThirstLevel; public float GetWorkingSpeed() => workingSpeed * GetPerformanceMultiplier();

    // ��������� ������������������ �� ������ ���������
    private float GetPerformanceMultiplier()
    {
        float stressFactor = _currentStressLevel / maxStressLevel;
        float hungerFactor = 1f - (_currentStarveLevel / maxStarveLevel);
        float thirstFactor = 1f - (_currentThirstLevel / maxThirstLevel);

        return Mathf.Clamp((stressFactor + hungerFactor + thirstFactor) / 3f, 0.2f, 1f);
    }
}
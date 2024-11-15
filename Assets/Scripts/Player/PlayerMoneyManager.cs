using UnityEngine;

public class PlayerMoneyManager : MonoBehaviour
{
    // Синглтон
    public static PlayerMoneyManager Instance { get; private set; }

    [Header("Баланс игрока")]
    [SerializeField] private int currentBalance = 50;

    [Header("Максимальный баланс")]
    [SerializeField] private int maxBalance = 1000;

    [Header("Прогресс-бар")]
    [SerializeField] private ProgressBar progressBar;


    // Начальный баланс
    private int startingBalance = 50;


    // Событие при изменении баланса
    public event System.Action<int> OnBalanceChanged;

    private void Awake()
    {
        // Проверка на наличие другого экземпляра
        if (Instance != null && Instance != this)
        {
            Debug.LogError("PlayerMoneyManager: обнаружен второй экземпляр. Уничтожаем лишний.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Сохраняем объект между сценами
    }

    private void Start()
    {
        NotifyBalanceChanged();
    }

    public void AddMoney(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("Сумма для добавления должна быть положительной.");
            return;
        }

        currentBalance = currentBalance + amount;
        NotifyBalanceChanged();
    }

    public void SpendMoney(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("Сумма для вычитания должна быть положительной.");
            return;
        }

        currentBalance = Mathf.Max(currentBalance - amount, 0);
        NotifyBalanceChanged();
    }

    public int GetCurrentBalance()
    {
        return currentBalance;
    }

    private void NotifyBalanceChanged()
    {
        OnBalanceChanged?.Invoke(currentBalance);

        if (progressBar != null && progressBar.gameObject.activeInHierarchy)
        {
            float progress = Mathf.Clamp01((float)currentBalance / maxBalance);
            progressBar.SetProgress(progress);

            if (currentBalance >= maxBalance)
            {
                progressBar.transform.parent.gameObject.SetActive(false);
                //progressBar.gameObject.SetActive(false);
                Debug.Log("Прогресс-бар отключён: достигнут максимальный баланс.");
            }
        }
    }

    public void ResetBalance()
    {
        currentBalance = startingBalance;

        if (progressBar != null)
        {
            progressBar.transform.parent.gameObject.SetActive(true);
           // progressBar.gameObject.SetActive(true); // Включаем прогресс-бар
        }

        NotifyBalanceChanged();
    }

    private void OnDestroy()
    {
        // Удаляем ссылку на синглтон при разрушении объекта
        if (Instance == this)
        {
            Instance = null;
        }
    }
}

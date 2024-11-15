using UnityEngine;

public class PlayerMoneyManager : MonoBehaviour
{
    // ��������
    public static PlayerMoneyManager Instance { get; private set; }

    [Header("������ ������")]
    [SerializeField] private int currentBalance = 50;

    [Header("������������ ������")]
    [SerializeField] private int maxBalance = 1000;

    [Header("��������-���")]
    [SerializeField] private ProgressBar progressBar;


    // ��������� ������
    private int startingBalance = 50;


    // ������� ��� ��������� �������
    public event System.Action<int> OnBalanceChanged;

    private void Awake()
    {
        // �������� �� ������� ������� ����������
        if (Instance != null && Instance != this)
        {
            Debug.LogError("PlayerMoneyManager: ��������� ������ ���������. ���������� ������.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // ��������� ������ ����� �������
    }

    private void Start()
    {
        NotifyBalanceChanged();
    }

    public void AddMoney(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("����� ��� ���������� ������ ���� �������������.");
            return;
        }

        currentBalance = currentBalance + amount;
        NotifyBalanceChanged();
    }

    public void SpendMoney(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("����� ��� ��������� ������ ���� �������������.");
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
                Debug.Log("��������-��� ��������: ��������� ������������ ������.");
            }
        }
    }

    public void ResetBalance()
    {
        currentBalance = startingBalance;

        if (progressBar != null)
        {
            progressBar.transform.parent.gameObject.SetActive(true);
           // progressBar.gameObject.SetActive(true); // �������� ��������-���
        }

        NotifyBalanceChanged();
    }

    private void OnDestroy()
    {
        // ������� ������ �� �������� ��� ���������� �������
        if (Instance == this)
        {
            Instance = null;
        }
    }
}

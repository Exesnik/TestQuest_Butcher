using UnityEngine;
using TMPro;

public class PlayerWealthLevel : MonoBehaviour
{
    public int poor = 50;
    public int wealthy = 250;
    public int rich = 500;

    [SerializeField] private TextMeshProUGUI statusText;

    private void Start()
    {
        if (statusText == null)
        {
            Debug.LogError("������ �� �������� � ����������");
            return;
        }

        PlayerMoneyManager.Instance.OnBalanceChanged += HandleBalanceChanged;
        UpdateStatus(PlayerMoneyManager.Instance.GetCurrentBalance()); // ������������� ������� ��� ������
    }

    private void HandleBalanceChanged(int newBalance)
    {
        UpdateStatus(newBalance);
    }

    private void UpdateStatus(int balance)
    {
        if (balance < poor)
        {
            statusText.text = "������";
            statusText.color = Color.red;
        }
        else if (balance < wealthy)
        {
            statusText.text = "�������������";
            statusText.color = Color.yellow;
        }
        else if (balance < rich)
        {
            statusText.text = "�������";
            statusText.color = Color.green;
        }

    }

    private void OnDestroy()
    {
        // ������������ �� ������� ��� ����������� �������
        if (PlayerMoneyManager.Instance != null)
        {
            PlayerMoneyManager.Instance.OnBalanceChanged -= HandleBalanceChanged;
        }
    }
}

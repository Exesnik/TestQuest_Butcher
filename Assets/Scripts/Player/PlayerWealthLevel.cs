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
            Debug.LogError("Статус не обьявлен в инспекторе");
            return;
        }

        PlayerMoneyManager.Instance.OnBalanceChanged += HandleBalanceChanged;
        UpdateStatus(PlayerMoneyManager.Instance.GetCurrentBalance()); // Инициализация статуса при старте
    }

    private void HandleBalanceChanged(int newBalance)
    {
        UpdateStatus(newBalance);
    }

    private void UpdateStatus(int balance)
    {
        if (balance < poor)
        {
            statusText.text = "Бедняк";
            statusText.color = Color.red;
        }
        else if (balance < wealthy)
        {
            statusText.text = "Состоятельный";
            statusText.color = Color.yellow;
        }
        else if (balance < rich)
        {
            statusText.text = "Богатый";
            statusText.color = Color.green;
        }

    }

    private void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта
        if (PlayerMoneyManager.Instance != null)
        {
            PlayerMoneyManager.Instance.OnBalanceChanged -= HandleBalanceChanged;
        }
    }
}

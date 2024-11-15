using UnityEngine;

public class Dollar : MonoBehaviour, IPickup
{
    [Header("�������")]
    public int value = 1; 

    private PlayerMoneyManager playerMoneyManager;

    public AudioClip pickupSound;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = pickupSound;

        try
        {
            playerMoneyManager = FindObjectOfType<PlayerMoneyManager>();
        }
        catch (System.Exception)
        {
            Debug.LogError("�������� �� ������");
            throw;
        }    
        
    }

    public void Pickup()
    {
        if (playerMoneyManager != null)
        {
            playerMoneyManager.AddMoney(value);
            Debug.Log($"��������� ������!");   
            audioSource.PlayOneShot(pickupSound);
            Destroy(gameObject, 0.15f);
           
        }
        else
        {
            Debug.LogWarning("PlayerMoneyManager �� ������!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ��� ����� �������� ������
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alcohol : MonoBehaviour , IPickup
{
    [Header("�������, ������� ��� ����� ���������")]
    public int value = 20;

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
        audioSource.PlayOneShot(pickupSound);

        if (playerMoneyManager != null)
        {
            playerMoneyManager.SpendMoney(value);
            Debug.Log($"��������� �������!");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleZone : MonoBehaviour
{
    [SerializeField]
    public int multX = 1;
    private Animator doorAnimator; 

    private void Start()
    {
        doorAnimator = GetComponent<Animator>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMoneyManager.Instance.AddMoney(multX * PlayerMoneyManager.Instance.GetCurrentBalance());
            doorAnimator.SetTrigger("OpenDoor"); 
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public AudioClip checkpointSound;
    private AudioSource audioSource;

    private Animator checkPointAnimator;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = checkpointSound;

        checkPointAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Чекпойнт");
            audioSource.PlayOneShot(checkpointSound);

            checkPointAnimator.SetTrigger("CheckPoint");

        }

    }
}

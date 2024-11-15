using UnityEngine;

public class FinishZone : MonoBehaviour
{
    [SerializeField] private Transform finishPoint;
    [SerializeField] private float moveDuration = 1f;
    private float rotationProgress = 0f;
    private Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;

            // Отключаем управление и блокируем движение
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.BlockMovement();
            }

            rotationProgress = 0f;
            GetFinish();
        }
    }

    private void FixedUpdate()
    {
        TransformTOPoint();
    }

    private void TransformTOPoint()
    {
        if (player != null)
        {
            rotationProgress += Time.deltaTime;
            float t = Mathf.Clamp01(rotationProgress / moveDuration);
            player.position = Vector3.Lerp(player.position, finishPoint.position, t);
            if (t >= 1f)
            {
                player = null;
            }
        }
    }

    private void GetFinish()
    {
        Debug.Log("Дошёл до финиша");
    }
}

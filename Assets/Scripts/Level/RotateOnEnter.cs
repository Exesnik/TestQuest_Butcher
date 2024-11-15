using UnityEngine;

public class RotateOnEnter : MonoBehaviour
{
    public float rotationDuration = 1.0f;
    public bool leftRotation = false;
    private bool isRotating = false;
    private float rotationProgress = 0f;
    private Quaternion targetRotation;

    private void OnTriggerEnter(Collider other)
    {
        CheckPlayer(other);
    }

    private void FixedUpdate()
    {
        PlayerRotate();
    }

    private void CheckPlayer(Collider other)
    {
        if (!isRotating && other.CompareTag("Player"))
        {
            isRotating = true;


            float rotationAngle = leftRotation ? -90f : 90f;        
            targetRotation = Quaternion.Euler(0, rotationAngle, 0) * other.transform.rotation;

            rotationProgress = 0f; 
        }
    }

    private void PlayerRotate()
    {
        if (isRotating)
        {
            rotationProgress += Time.deltaTime / rotationDuration;
            if (rotationProgress >= 1f)
            {
                rotationProgress = 1f;
                isRotating = false;
            }

            Transform characterTransform = GameObject.FindGameObjectWithTag("Player").transform;
            characterTransform.rotation = Quaternion.Slerp(characterTransform.rotation, targetRotation, rotationProgress);
        }
    }
}
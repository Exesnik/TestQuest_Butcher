using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 8f;
    public float horizontalSpeed = 8f;

    private bool isMoving = false;
    private bool isMovementBlocked = false; // ���� ���������� ��������

    void Start()
    {
        isMoving = false;
    }

    void Update()
    {
        if (isMoving && !isMovementBlocked)
        {
            MovePlayer();
        }
    }


    public void SetMovingState(bool moving)
    {
        if (!isMovementBlocked)
        {
            isMoving = moving;
        }
    }


    public void BlockMovement()
    {
        isMovementBlocked = true;
        isMoving = false; 
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // �������� �� �����������
        transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime);

        // �������� ������
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }
}

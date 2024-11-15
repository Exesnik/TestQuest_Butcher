using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        try
        {
            playerController = GetComponent<PlayerController>();
        }
        catch (System.Exception)
        {
            Debug.LogError("Контроллер не найден");
            throw;
        }

        playerController.SetMovingState(false);
    }

    void Update()
    {      
        CheckForMovementInput();
    }

    private void CheckForMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {       
            playerController.SetMovingState(true);
        }
    }
}
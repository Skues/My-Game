using UnityEngine;

public class CrouchController : MonoBehaviour
{
    public Camera playerCamera;
    public CharacterController characterController;
    private float crouchHeight = 1.4f;
    private float standHeight = 3.71f;
    public float smoothSpeed = 10f;
    private float targetHeight;
    private Vector3 targetCameraPosition;
    private Vector3 currentCameraPosition;

    private bool isCrouching = false;

    void Start()
    {
        targetHeight = standHeight;
        currentCameraPosition = playerCamera.transform.localPosition;
        targetCameraPosition = currentCameraPosition;
    }

    void Update()
    {
        // Toggle crouch on key press (e.g., C key)
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCrouch();
        }

        // Smoothly update the camera position and character controller height
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, smoothSpeed * Time.deltaTime);
        playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, targetCameraPosition, smoothSpeed * Time.deltaTime);
    }

    private void ToggleCrouch()
    {
        if (isCrouching)
        {
            targetHeight = standHeight;
            targetCameraPosition = new Vector3(playerCamera.transform.localPosition.x, 1.48f, playerCamera.transform.localPosition.z);
        }
        else
        {
            targetHeight = crouchHeight;
            targetCameraPosition = new Vector3(playerCamera.transform.localPosition.x, 1f , playerCamera.transform.localPosition.z);
        }

        isCrouching = !isCrouching;
    }
    public bool IsCrouching(){
        return isCrouching;
    }
}
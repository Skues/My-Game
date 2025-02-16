using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool isCrouching;
    Vector3 targetPosition;
    Camera mainCamera;
    Vector3 cameraPosition;
    public CharacterController controller;
    public float smoothSpeed = 0.5f;
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private float crouchHeight = 0.9f; // How much the camera should move down when crouching
    private Transform player; // The player's transform
 
    Vector3 velocity;
 
    bool isGrounded;
    void Start()
    {
        mainCamera = Camera.main;   
        player = transform;
        cameraPosition = mainCamera.transform.position;
        targetPosition = cameraPosition;

    }

    // Update is called once per frame
    void Update()
    {
        //checking if we hit the ground to reset our falling velocity, otherwise we will fall faster the next time
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
 
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
 
        //right is the red Axis, foward is the blue axis
        Vector3 move = transform.right * x + transform.forward * z;
 
        controller.Move(move * speed * Time.deltaTime);
 
        //check if the player is on the ground so he can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //the equation for jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            
        }

         // Toggle crouch/stand on key press (C)
        if (Input.GetKeyDown(KeyCode.C) && isGrounded)
        {
            if (isCrouching)
            {
                // Stand up (move camera back up)
                targetPosition = new Vector3(cameraPosition.x, cameraPosition.y + crouchHeight, cameraPosition.z);
                speed = 12f;
                isCrouching = false;
            }
            else
            {
                // Start crouching (move camera down)
                targetPosition = new Vector3(cameraPosition.x, cameraPosition.y - crouchHeight, cameraPosition.z);
                speed = 5f;
                isCrouching = true;
            }
        }

        // Smoothly transition the camera to the target position
        cameraPosition = Vector3.Lerp(cameraPosition, targetPosition, smoothSpeed);

        // Apply the camera position update to the camera
        mainCamera.transform.position = new Vector3(player.position.x, player.position.y + (cameraPosition.y - transform.position.y), player.position.z);

        velocity.y += gravity * Time.deltaTime;
 
        controller.Move(velocity * Time.deltaTime);
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool isCrouching;
    Vector3 targetPosition;
    Camera mainCamera;
    Vector3 cameraPosition;
    public CharacterController controller;
    private CrouchController crouchController;
    public float smoothSpeed = 0.5f;
    public float baseSpeed = 9f;
    public float currentSpeed;
    public float sprint = 5f;
    public float crouchSpeed = 5f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private Transform player; // The player's transform
 
    Vector3 velocity;
 
    bool isGrounded;
    void Start()
    {
        crouchController = GetComponent<CrouchController>();
        mainCamera = Camera.main;   
        player = transform;
        cameraPosition = mainCamera.transform.position;
        targetPosition = cameraPosition;

    }

    // Update is called once per frame
    void Update()
    {
        bool isCrouching = crouchController.IsCrouching();
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching){
            currentSpeed = baseSpeed + sprint;
        }
        else if(isCrouching){
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentSpeed = baseSpeed;
        }
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
 
        controller.Move(move * currentSpeed * Time.deltaTime);
 
        //check if the player is on the ground so he can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //the equation for jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

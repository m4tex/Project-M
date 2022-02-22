using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    //sprint
    public float sprintSpeedMultiplier;
    private bool isRunning;
    //Stamina
    public float maxStamina, stamina;
    public float staminaDelay, staminaDelayCounter;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    //Stops movement when in pause menu
    int pauseMultiplier = 1;
    bool isPaused;

    //Crouching lying variables
    public bool isCrouching;
    public bool isLying;
    public float lieTime, lieTimeCounter;
    public bool pressedOnce, liedOnce;

    private void Update()
    {
        WalkingAndJumping();
        LyingAndCrouching();
    }

    public void ESCPause()
    {
        if (!isPaused)
        {
            pauseMultiplier = 0;
            isPaused = true;
        }
        else
        {
            pauseMultiplier = 1;
            isPaused = false;
        }
    }

    void WalkingAndJumping()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal") * pauseMultiplier;
        float z = Input.GetAxis("Vertical") * pauseMultiplier;

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed *  Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        
        //Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift) && x + z != 0 && staminaDelayCounter <= 0 && !isRunning)
        {
            speed *= sprintSpeedMultiplier;
            isRunning = true;
        }
        if(isRunning && x + z == 0)
        {
            speed /= sprintSpeedMultiplier;
            isRunning = false;
        }
        if(isRunning && stamina <= 0)
        {
            speed /= sprintSpeedMultiplier;
            isRunning = false;
            staminaDelayCounter = staminaDelay;
        }
        
        //Stamina
        if (isRunning)
            stamina -= Time.deltaTime;
        if (stamina < maxStamina && !isRunning)
            stamina += Time.deltaTime / 2;
        if (staminaDelayCounter > 0)
            staminaDelayCounter -= Time.deltaTime;
    }

    void LyingAndCrouching()
    {
        if (Input.GetKeyDown(KeyCode.C) && pressedOnce)
            pressedOnce = false;
        else
        {
            if (Input.GetKeyDown(KeyCode.C) && !pressedOnce)
                pressedOnce = true;
        }
        if(Input.GetKeyUp(KeyCode.C) && liedOnce)
            liedOnce = false;
        if (Input.GetKeyDown(KeyCode.C) && !isCrouching)
        {
            Crouch();
            isCrouching = true;
            isLying = false;
            lieTimeCounter = lieTime;
        }
        else
        if(Input.GetKeyUp(KeyCode.C) && isCrouching && !pressedOnce && !liedOnce)
        {
            StandUp();
            isCrouching = false;
            isLying = false;
            lieTimeCounter = lieTime;
        }
        else
        if (Input.GetKey(KeyCode.C) && !isLying)
        {
            lieTimeCounter -= Time.deltaTime;
            if (lieTimeCounter <= 0)
            {
                Lie();
                isLying = true;
                isCrouching = false;
                liedOnce = true;
            }
        }
    }
    
    private void Crouch()
    {
        print("crouching");
    }

    private void Lie()
    {
        print("lying");
    }

    private void StandUp()
    {
        print("standing up");
    }
}
using System;
using UnityEngine;
using Playgama;
using System.Collections.Generic;

public class ThirdPersonController : MonoBehaviour
{
    [Tooltip("Speed ​​at which the character moves. It is not affected by gravity or jumping.")]
    public float speed = 5f;
    [Tooltip("This value is added to the speed value while the character is sprinting.")]
    public float sprintAdittion = 3.5f;
    [Tooltip("The higher the value, the higher the character will jump.")]
    public float jumpForce = 18f;
    [Tooltip("Stay in the air. The higher the value, the longer the character floats before falling.")]
    public float jumpTime = 0.85f;
    [Space]
    [Tooltip("Force that pulls the player down. Changing this value causes all movement, jumping and falling to be changed as well.")]
    public float gravity = 9.8f;

    float jumpElapsedTime = 0;


    [SerializeField] private Joystick  _joystick;

    private float smoothTime;
    [SerializeField] private float smoothVelocity;
    [SerializeField] private Transform _firstCamera;


    [SerializeField] AudioSource jumpSound;

    // Player states
    bool isJumping = false;
    //bool isSprinting = false;
    //bool isCrouching = false;

    bool jumpMobile;

    // Inputs
    float inputHorizontal;
    float inputVertical;
    bool inputJump;
    bool inputCrouch;
    bool inputSprint;

    [SerializeField] Animator animator;
    [SerializeField] CharacterController cc;

    public bool CanMove;


    private void Awake()
    {
        speed = GameData.speed;
        if (speed == 0)
        {
            speed = 10f;
        }
        Debug.Log("Скорость: " + speed);

        
    }

    public void  ChangeSpeed(float value)
    {
        speed += value;
        GameData.speed = speed;
        PlayerPrefs.SetFloat("speed", speed);
        Save("speed", speed);
    }
   

    private void OnEnable()
    {
        EventManager.LevelCompleted += DeactivatePlayer;
        EventManager.GameCompleted += DeactivatePlayer;
    }


    private void OnDisable()
    {
        EventManager.LevelCompleted -= DeactivatePlayer;
        EventManager.GameCompleted -= DeactivatePlayer;

    }

    void DeactivatePlayer()
    {
        gameObject.SetActive(false);
    }
 

    


    // Update is only being used here to identify keys and trigger animations
    void Update()
    {




        
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputJump = Input.GetAxis("Jump") == 1f;
        
     

        if(inputHorizontal != 0 || inputVertical != 0)
        {
            animator.SetBool("run", true);
        }

        else
        {
            animator.SetBool("run", false);

        }





        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded)
        {
            ActiveAnimJump();
            jumpSound.Play();


        }
    }



    void ActiveAnimJump()
    {
        isJumping = true;
       // animator.SetBool("jump", true);

    }

    void DeactiveAnimJump()
    {
        animator.SetBool("jump", false);

    }


    public void OnMobileJump()
    {
        if (cc.isGrounded)
        {
            isJumping = true;
            jumpSound.Play();

        }

    }

    
    
    private void FixedUpdate()
    {
        if (CanMove) TryMove();
    }


    void TryMove()
    {
        JoystickInputMove();

        // Sprinting velocity boost or crounching desacelerate
        float velocityAdittion = 0;


        // Direction movement
        float directionX = inputHorizontal * (speed + velocityAdittion) * Time.deltaTime;
        float directionZ = inputVertical * (speed + velocityAdittion) * Time.deltaTime;
        float directionY = 0;

        // Jump handler
        if (isJumping)
        {
            animator.SetBool("jump", true);

            // Apply inertia and smoothness when climbing the jump
            // It is not necessary when descending, as gravity itself will gradually pulls
            directionY = Mathf.SmoothStep(jumpForce, jumpForce * 0.30f, jumpElapsedTime / jumpTime) * Time.deltaTime;

            // Jump timer
            jumpElapsedTime += Time.deltaTime;
            if (jumpElapsedTime >= jumpTime)
            {
                isJumping = false;
                jumpElapsedTime = 0;
            }
        }

        else
        {
            animator.SetBool("jump", false);
        }

        // Add gravity to Y axis
        directionY = directionY - gravity * Time.deltaTime;


        // --- Character rotation --- 

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Relate the front with the Z direction (depth) and right with X (lateral movement)
        forward = forward * directionZ;
        right = right * directionX;

        if (directionX != 0 || directionZ != 0)
        {
            float angle = Mathf.Atan2(forward.x + right.x, forward.z + right.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }

        // --- End rotation ---


        Vector3 verticalDirection = Vector3.up * directionY;
        Vector3 horizontalDirection = forward + right;

        Vector3 movment = verticalDirection + horizontalDirection;
        cc.Move(movment);
    }

    void JoystickInputMove()
    {
        Vector3 dirJoystick = new Vector3(_joystick.Horizontal, 0f,_joystick.Vertical).normalized;

        

        if (dirJoystick.magnitude >= 0.1f)
        {

            float rotationAngle = Mathf.Atan2(dirJoystick.x, dirJoystick.z) * Mathf.Rad2Deg + _firstCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 move = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;

            cc.Move(move.normalized * speed * Time.deltaTime);


        }


        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            animator.SetBool("runMobile", true);
        }


        else
        {
            animator.SetBool("runMobile", false);

        }
    }
    
    
    void Save(string name, float value)
    {
        var key = new List<string> { name };
        var data = new List<object> { value };
        Bridge.storage.Set(key, data, OnStorageSetCompleted);
    }
    
    private void OnStorageSetCompleted(bool success)
    {
        Debug.Log($"OnStorageSetCompleted, success: {success}");
    }
}

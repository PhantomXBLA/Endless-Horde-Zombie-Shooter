using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    [SerializeField]
    float walkSpeed = 5;
    [SerializeField]
    float runSpeed = 10;
    [SerializeField]
    float jumpForce = 5;

    public float maxFlightTime = 8;
    public float flightTime;

    private PlayerController playerController;

    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;
    Vector2 lookInput = Vector2.zero;

    public float aimSensitivity = 0.05f;

    Rigidbody rigidbody;
    Animator playerAnimator;

    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int isRunningHash = Animator.StringToHash("IsRunning");


    public GameObject FollowTarget;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        flightTime = maxFlightTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.x * aimSensitivity, Vector3.up);
        FollowTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.y * aimSensitivity, Vector3.left);

        var angles = FollowTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = FollowTarget.transform.localEulerAngles.x;

        if (angle > 180 && angle < 300)
        {
            angles.x = 300;
        }

        else if (angle < 180 && angle > 70)
        {
            angles.x = 70;
        }

        FollowTarget.transform.localEulerAngles = angles;

        transform.rotation = Quaternion.Euler(0, FollowTarget.transform.rotation.eulerAngles.y, 0);

        FollowTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);


       // if (playerController.isJumping) return;

        if (!(inputVector.magnitude > 0)) moveDirection = Vector3.zero;

        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;
        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

        transform.position += movementDirection;

        if(flightTime <= 0 && playerController.isJumping == true)
        {
            rigidbody.useGravity = true;
        }


    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        playerAnimator.SetFloat(movementXHash, inputVector.x);
        playerAnimator.SetFloat(movementYHash, inputVector.y);
    }

    public void OnJump(InputValue value)
    {
        playerController.isJumping = true;
        //rigidbody.AddForce((transform.up + moveDirection) * jumpForce, ForceMode.Impulse);
        rigidbody.useGravity = false;

        if (value.isPressed && flightTime > 0)
        {
            InvokeRepeating(nameof(FlyUP), 0.1f, 0.01f);
            InvokeRepeating(nameof(decreaseFlightTimer), 0.25f, 1);
        }
        else
        {
            CancelInvoke(nameof(FlyUP));
        }


        
        playerAnimator.SetBool(isJumpingHash, playerController.isJumping);

    }

    void FlyUP()
    {
        this.gameObject.transform.position += new Vector3(0, 0.05f, 0);
    }

    void decreaseFlightTimer()
    {
       
        flightTime--;

        if(flightTime < 0)
        {
            flightTime = 0;
        }

        print(flightTime);
    }

    public void OnAim(InputValue value)
    {
        playerController.isAiming = value.isPressed;
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }



    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        playerAnimator.SetBool(isRunningHash, playerController.isRunning);

    }

    public void OnDescend(InputValue value)
    {
        rigidbody.useGravity = true;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (!collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false; 
        playerAnimator.SetBool(isJumpingHash, false);
        CancelInvoke(nameof(decreaseFlightTimer));
    }
}

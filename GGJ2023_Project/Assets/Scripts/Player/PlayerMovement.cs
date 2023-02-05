using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float slowSpeed;
    public float runSpeed;
    [ReadOnly] public bool isSlow;
    [ReadOnly] public bool isRunning;

    private float walkSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode slowDownKey = KeyCode.LeftControl;
    public KeyCode runKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private Vector3 startPosition;

    public event Action OnPlayerDeath;

    private void Start()
    {
        walkSpeed = moveSpeed;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        startPosition = transform.position;
    }

    public void RestorePosition()
    {
        transform.position = startPosition;
        OnPlayerDeath?.Invoke();
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKeyDown(slowDownKey) && grounded && !isRunning)
        {
            SlowDown();
        }
        if (Input.GetKeyUp(slowDownKey) && grounded && !isRunning)
        {
            ReleaseSlowDown();
        }
        if (Input.GetKeyDown(runKey) && grounded && !isSlow)
        {
            StartRunning();
        }
        if (Input.GetKeyUp(runKey) && grounded && !isSlow)
        {
            StopRunning();
        }
    }

    private void SlowDown()
    {
        if (!isSlow)
        {
            walkSpeed = slowSpeed;
            isSlow = true;
        }
    }

    private void ReleaseSlowDown()
    {
        if (isSlow)
        {
            walkSpeed = moveSpeed;
            isSlow = false;
        }
    }

    private void StartRunning()
    {
        if (!isRunning)
        {
            isRunning = true;
            walkSpeed = runSpeed;
        }
    }

    private void StopRunning()
    {
        if (isRunning)
        {
            isRunning = false;
            walkSpeed = moveSpeed;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * walkSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * walkSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
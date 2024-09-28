using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // variables for player input from new input system
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;

    // player speed
    [SerializeField]
    private float speed;

    // all the jump variables
    [SerializeField]
    private float jumpRaycastDistance;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float jumpHeight;
    private bool isGrounded;
    [SerializeField]
    private Transform groundCheck;
    private float groundCheckRadius = 0.2f;
    [SerializeField]
    private float jumpForce;

    private Rigidbody rb;

    // some movement variables
    [SerializeField]
    private Transform cameraMovement;
    private Vector3 forwardCam;
    private Vector3 crightCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        jumpAction = playerInput.actions.FindAction("Jump");

        jumpAction.started += JumpAction_started;
        jumpAction.canceled += JumpAction_canceled;
    }

    private void JumpAction_canceled(InputAction.CallbackContext obj)
    {
        Debug.Log("stopped jumping");
    }

    private void JumpAction_started(InputAction.CallbackContext obj)
    {
        Jump();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        //IsGrounded();
    }

    // this function gets the direction that the player should be moving in then transforms it's position
    void MovePlayer()
    {
        forwardCam = cameraMovement.forward;
        crightCamera = cameraMovement.right;

        forwardCam.y = 0;
        crightCamera.x = 0;

        forwardCam.Normalize();
        crightCamera.Normalize();

        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = (crightCamera * direction.x + forwardCam * direction.y).normalized;
        transform.position += moveDirection * Time.deltaTime * speed;
    }

    // checks if the player is on the ground
    //private bool IsGrounded()
    //{
       
    //}
    
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnDestroy()
    {
        jumpAction.started -= JumpAction_started;
        jumpAction.canceled -= JumpAction_canceled;
    }

    //[SerializeField]
    //private float speed;
    //[SerializeField]
    //private Transform orientation;

    //private float horizontalInput;
    //private float verticalInput;

    //Vector3 moveDirection;

    //Rigidbody rb;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    rb.freezeRotation = true;
    //}

    //private void Update()
    //{
    //    keyboardCalls();
    //}

    //private void FixedUpdate()
    //{
    //    movePlayer();
    //}

    //private void keyboardCalls()
    //{
    //    horizontalInput = Input.GetAxisRaw("Horizontal");
    //    verticalInput = Input.GetAxisRaw("Vertical");

    //}

    //private void movePlayer()
    //{
    //    moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
    //    rb.AddForce(moveDirection * speed,ForceMode.Force);
    //}

}

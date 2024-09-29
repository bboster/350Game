using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerMovementActionMap playerInput;
    public PlayerMovementActionMap.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;
    

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerMovementActionMap();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => motor.Jump(); // any time jump is performed, we use callback context to call motor.jump action
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // tells the player motor to move using the movement action input
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}

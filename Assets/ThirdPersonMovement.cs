using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;

    public InputMaster controls;

    public float speed = 6f;

    public Vector2 move;
    public Transform cam;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Jump.performed += _ => Jump();
        controls.Player.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => move = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Move(Vector2 input)
    {
        
    }

    void Jump()
    {
        Debug.Log("Jump");
    }


    
    
    private void Update()
    {
        float horizontal = move.x;
        float vertical = move.y;
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        Debug.Log("Moving");
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0);

            Vector3 moveDirecetion = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDirecetion.normalized * speed * Time.deltaTime);
        }
    }
}

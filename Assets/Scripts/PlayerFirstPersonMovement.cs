using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirstPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    public InputMaster controls;


    public float speed = 6f;

    public Vector2 moveInput;

    private void Awake()
    {
        controls = new InputMaster();
        //controls.Player.Jump.performed += _ => Jump();
        controls.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.LeftClick.performed += _ => Shoot();


    }

    private void Shoot()
    {
        GetComponent<Laser>().Shoot();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = controller.transform.right * moveInput.x + controller.transform.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);
    }
}

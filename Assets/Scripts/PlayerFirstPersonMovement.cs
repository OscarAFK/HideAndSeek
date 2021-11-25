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
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);
    }
}

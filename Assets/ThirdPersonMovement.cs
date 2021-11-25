using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;

public class ThirdPersonMovement : NetworkBehaviour
{
    public enum PlayerState{
        Idle,
        Walking
    }

    private NetworkVariable<PlayerState> networkPlayerState = new NetworkVariable<PlayerState>();

    public Animator animator;

    public CharacterController controller;

    public InputMaster controls;


    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity = 0.1f;

    public Vector2 move;

    public Transform cam;

    //variables pour la gestion des animations
    Vector3 lastPos;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Jump.performed += _ => Jump();
        controls.Player.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => move = Vector2.zero;
        cam = Camera.main.transform;
        lastPos = transform.position;
        
    }

    private void Start()
    {
        if (GetComponent<NetworkObject>().IsLocalPlayer)
        {
            var cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
            cinemachineFreeLook.Follow = transform;
            cinemachineFreeLook.LookAt = transform.Find("HeadTransf");
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Jump()
    {
        Debug.Log("Jump");
    }


    
    
    private void Update()
    {
        MovePlayer();
        AnimatePlayer();
    }

    void MovePlayer()
    {
        Vector3 direction;

        float horizontal = move.x;
        float vertical = move.y;
        direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            if(gameObject.GetComponent<NetworkObject>().IsOwner) UpdatePlayerStateServerRPC(PlayerState.Walking);//animator.SetBool("isMoving", true);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0);

            Vector3 moveDirecetion = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDirecetion.normalized * speed * Time.deltaTime);
        }else
        {
            if (gameObject.GetComponent<NetworkObject>().IsOwner) UpdatePlayerStateServerRPC(PlayerState.Idle);//animator.SetBool("isMoving", false);
        }
    }

    void AnimatePlayer()
    {
        if(networkPlayerState.Value == PlayerState.Walking)
        {
            animator.SetBool("isMoving", true);
        }else
        {
            animator.SetBool("isMoving", false);
        }
    }

    [ServerRpc]
    public void UpdatePlayerStateServerRPC(PlayerState newState)
    {
        networkPlayerState.Value = newState;
    }
}

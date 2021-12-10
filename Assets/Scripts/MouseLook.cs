using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 10f;

    public Transform playerBody;

    float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Awake()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.menuPause.activeSelf)
        {
            float mouseX = Mouse.current.delta.x.ReadValue() * Time.deltaTime * mouseSensitivity;
            float mouseY = Mouse.current.delta.y.ReadValue() * Time.deltaTime * mouseSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}

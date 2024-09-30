using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NonVR_RigLook : MonoBehaviour
{
    private PlayerInput controls;

    [SerializeField] private float lookSpeed = 100f;
    private Vector2 mouseLook;
    
    private float xRotation = 0f;

    private Transform playerBody;

    void Awake() {
        playerBody = transform.parent;

        controls = new PlayerInput();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    private void Look() {
        mouseLook = controls.Player.Look.ReadValue<Vector2>();

        float mouseX = mouseLook.x * lookSpeed * Time.deltaTime;
        float mouseY = mouseLook.y * lookSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

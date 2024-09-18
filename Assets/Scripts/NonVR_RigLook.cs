using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.InputSystem;

public class NonVR_RigLook : MonoBehaviour
{
    [SerializeField] private InputActionReference horizontalLook;
    [SerializeField] private InputActionReference verticalLook;

    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float lookSpeed = 1f;
    [SerializeField] private Transform cameraTransform;

    private float pitch;
    private float yaw;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        horizontalLook.action.performed += HandleHorizontalLookChange;
        verticalLook.action.performed += HandleVerticalLookChange;

    }

    void HandleHorizontalLookChange(InputAction.CallbackContext obj) {
        yaw += obj.ReadValue<float>();
        transform.localRotation = Quaternion.AngleAxis(yaw * lookSpeed, Vector3.up);
    }

    void HandleVerticalLookChange(InputAction.CallbackContext obj) {
        pitch -= obj.ReadValue<float>();
        transform.localRotation = Quaternion.AngleAxis(pitch * lookSpeed, Vector3.right);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

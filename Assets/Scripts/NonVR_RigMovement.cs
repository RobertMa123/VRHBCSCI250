using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NonVR_RigMovement : MonoBehaviour
{
    private PlayerInput controls;
    [SerializeField] private float moveSpeed = 6f;

    private Vector2 move;

    private CharacterController controller;

    void Awake() {
        controls = new PlayerInput();
        controller = GetComponent<CharacterController>();
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
        PlayerMovement();
    }

    private void PlayerMovement() {
        move = controls.Player.Movement.ReadValue<Vector2>();

        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }
}

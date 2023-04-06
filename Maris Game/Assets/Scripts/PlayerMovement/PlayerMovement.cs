using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float sprintMultiplier;

    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    

    private Vector3 velocity;
    private bool grounded;
    
    private InputManager inputManager;
    private GameManager gameManager;

    private void Start() {
        gameManager = GameManager.instance;
        inputManager = GameManager.instance.inputManager;
    }

    private void Update() {
        float x = 0f;
        float z = 0f;
        //Vertical Inputs
        if(Input.GetKey(inputManager.forwardKey)) {
            z = 1f;
        } else if(Input.GetKey(inputManager.backwardKey)) {
            z = -1f;
        }
        //Horizontal Inputs
        if(Input.GetKey(inputManager.rightKey)) {
            x = 1f;
        }
        else if(Input.GetKey(inputManager.leftKey)) {
            x = -1f;
        }
        Vector3 move = transform.right * x + transform.forward * z;

        if(Input.GetKey(inputManager.sprintKey)) {
            move *= sprintMultiplier;
        }
        controller.Move(move * speed * Time.deltaTime);

        
    }

    private void FixedUpdate() {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(grounded && velocity.y < 0) {
            velocity.y = 0f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity);
    }
}

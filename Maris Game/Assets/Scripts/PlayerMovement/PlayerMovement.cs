using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    

    private Vector3 velocity;
    private bool grounded;

    private void Update() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

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

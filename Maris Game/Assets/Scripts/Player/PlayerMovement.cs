using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public AudioSource audioSource;

    public float speed = 12f;
    public float sprintMultiplier;

    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    

    private Vector3 velocity;
    private bool grounded;
    public bool horizontal = false;
    public bool vertical = false;
    public bool sprinting = false;
    
    private InputManager inputManager;
    private GameManager gameManager;
    private Kleding kledingScript;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        kledingScript = GetComponent<Kleding>();
        gameManager = GameManager.instance;
        inputManager = GameManager.instance.inputManager;
    }

    private void Update() {
        float x = 0f;
        float z = 0f;
        //Vertical Inputs
        if(Input.GetKey(inputManager.forwardKey)) {
            z = 1f;
            vertical = true;
        } else if(Input.GetKey(inputManager.backwardKey)) {
            z = -1f;
            vertical = true;
        } else {
            vertical = false;
        }
        //Horizontal Inputs
        if(Input.GetKey(inputManager.rightKey)) {
            x = 1f;
            horizontal = true;
        }
        else if(Input.GetKey(inputManager.leftKey)) {
            x = -1f;
            horizontal = true;
        } else {
            horizontal = false;
        }
        Vector3 move = transform.right * x + transform.forward * z;

        if(Input.GetKey(inputManager.sprintKey)) {
            if(!kledingScript.mondkapjeOp) {
                sprinting = true;
                move *= sprintMultiplier;
            } else{
                sprinting = false;
            }  
        } else{
            sprinting = false;
        }
        controller.Move(move * speed * Time.deltaTime);


        if(horizontal && !sprinting || vertical && !sprinting ) {
            if(this.audioSource.clip.name == "Walking fast (sprint)") {
                this.audioSource.Stop();
            }

            if(!this.audioSource.isPlaying) {
                AudioClip clip = GameManager.instance.audioManager.FindClip("Walking");
                this.audioSource.clip = clip;
                this.audioSource.Play();
                Debug.Log("Play Walking Sound");
            }
            
        } else if(!horizontal && !vertical) {
            this.audioSource.Stop();
        }

        if(horizontal && sprinting || vertical && sprinting) {
            if(audioSource.clip.name == "Walking slow") {
                this.audioSource.Stop();
            }
            if(!this.audioSource.isPlaying) {
                AudioClip clip = GameManager.instance.audioManager.FindClip("Sprinting");
                this.audioSource.clip = clip;
                this.audioSource.Play();
            }
        }
        
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

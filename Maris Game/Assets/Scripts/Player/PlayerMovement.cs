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
    public bool walking = false;
    public bool sprinting = false;
    public bool canMove = true;
    
    private InputManager inputManager;
    private GameManager gameManager;
    private CapsuleCollider cc;
    public Kleding kledingScript;
    public Camera mainCam;
    public Camera sceneCam;

    [Header("Win Animation")]
    public Vector3 winPosition;
    public Quaternion winRotation;
    public float zoomSpeed;
    public float rotateSpeed;
    public float moveSpeed;
    public float duration;
    public bool won = false;
    public bool gameOver = false;

    private void Start() {
        cc = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
        gameManager = GameManager.instance;
        inputManager = GameManager.instance.inputManager;
    }

    private void Update() {

        if(canMove) {
            float x = 0f;
            float z = 0f;
            //Vertical Inputs
            if(Input.GetKey(inputManager.forwardKey)) {
                z = 1f;
                walking = true;
            } else if(Input.GetKey(inputManager.backwardKey)) {
                z = -1f;
                walking = true;
            } else {
                walking = false;
            }
            //Horizontal Inputs
            if(Input.GetKey(inputManager.rightKey)) {
                x = 1f;
                walking = true;
            }
            else if(Input.GetKey(inputManager.leftKey)) {
                x = -1f;
                walking = true;
            } else {
                walking = false;
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
        }


        if(walking && !sprinting) {
            if(this.audioSource.clip.name == "Walking fast ") {
                this.audioSource.Stop();
            }

            if(!this.audioSource.isPlaying) {
                AudioClip clip = GameManager.instance.audioManager.FindClip("Walking");
                this.audioSource.clip = clip;
                this.audioSource.Play();
                Debug.Log("Play Walking Sound");
            }
            
        } else if(!walking) {
            this.audioSource.Stop();
        }

        if(walking && sprinting) {
            if(audioSource.clip.name == "Walking slow ") {
                this.audioSource.Stop();
            }
            if(!this.audioSource.isPlaying) {
                AudioClip clip = GameManager.instance.audioManager.FindClip("Sprinting");
                this.audioSource.clip = clip;
                this.audioSource.Play();
            }
        }

        if(won) {
            if(transform.position == winPosition && transform.rotation == winRotation && sceneCam.fieldOfView == 39.6f) {
                GameManager.instance.Win();
            }
        }

        
    }

    IEnumerator Win() {
        cc.enabled = false;
        controller.enabled = false;
        won = true;
        canMove = false;
        Vector3 orgPos = transform.position;
        Quaternion orgRot = transform.rotation;
        float dur = Vector3.Distance(transform.position, winPosition) / duration; 
        float time = 0;
        
        while (time < duration) {
            time += Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(transform.position, winPosition, moveSpeed * Time.deltaTime);
            this.transform.rotation = Quaternion.RotateTowards(transform.rotation, winRotation, rotateSpeed * Time.deltaTime);
            sceneCam.fieldOfView = Mathf.MoveTowards(sceneCam.fieldOfView, 39.6f, zoomSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator GameOver() {
        Vector3 orgPos = transform.position;
        mainCam.GetComponent<MouseLook>().GameOver();
        cc.enabled = false;
        controller.enabled = false;
        gameOver = true;
        canMove = false;
        float time = 0f;
        float duration = 10f;

        while (time < duration) {
            time += Time.deltaTime;
            this.transform.position = orgPos;
            yield return null;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class AIController : MonoBehaviour
{
    public float visionDistance;
    private AudioSource audioSource;
 
    
    private GameObject player;
    private Kleding kledingScript;
    private NavMeshAgent agent;
    private SphereCollider sc;
    private Patrol patrol;

    public bool chase;
    public bool chasing;
    public bool seen;
    public bool inSight; 
    public float yOffset; 
    public Vector3 rayStartPos;
    public Vector3 rayDirection;
    public float fov;

    public string[] normalSounds;
    public string[] kapjeSounds;
    public string[] jasSounds;
    public string[] angrySounds;

    //patrol
    [Header("Patrol")]
    public float walkRange;

    [Header("Smooth Turning")]
    public float turnSpeed = 1f;
    private Coroutine lookCoroutine;
    private float curTurnTime = 0;


    [Header("Hearing")]
    public bool hearingPlayer = false;


    private void Start() {
        audioSource = GetComponent<AudioSource>();
        patrol = GetComponent<Patrol>();
        sc = GetComponent<SphereCollider>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        kledingScript = player.GetComponent<Kleding>();
    }

    private void Update() {
        if(hearingPlayer) {
            RotateToPlayer();
        }
        
        rayStartPos = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        rayDirection = player.transform.position - this.transform.position;

        RaycastHit hitInfo;
        
        //if the player is in the fov of the AI turn towards the player
        if(Vector3.Angle(rayDirection, transform.forward) < fov / 2) {
            if(Physics.Raycast (transform.position, rayDirection, out hitInfo, visionDistance)) {
                if(hitInfo.transform.tag == "Player") {
                    if(GameManager.instance.collectiblesCollected == 0) {
                        chasing = false;
                        seen = true;
                        inSight = true;
                    } else if(GameManager.instance.collectiblesCollected == 1) {
                        if(!kledingScript.mondkapjeOp && chase) {
                            chasing = true;
                            seen = true;
                            inSight = true;
                    } 
                    } else if(GameManager.instance.collectiblesCollected == 2) {
                        //if the player doesnt have their face mask on or have their jacket on, chase the player 
                        if(!kledingScript.mondkapjeOp && chase || kledingScript.jasAan && chase) {
                        chasing = true;
                        seen = true;
                        inSight = true;
                        } 
                    } else if(GameManager.instance.collectiblesCollected >= 3) {
                        patrol.enabled = false;
                        chasing = true;
                        seen = true;
                        inSight = true;

                    }
                    
                    Debug.DrawLine(rayStartPos, hitInfo.transform.position, Color.red);
                } else {
                    inSight = false;
                    chasing = false;
                    Debug.DrawLine(rayStartPos, hitInfo.transform.position, Color.yellow);
                    Debug.Log("Do Patrol");
                    patrol.DoPatrol();
                } 
                RotateToPlayer();   
            } else if(GameManager.instance.collectiblesCollected >= 3) {
                patrol.enabled = false;
                chasing = true;
                seen = true;
                inSight = true;

            } else {
                Debug.Log("Do Patrol");
                patrol.DoPatrol();
            }        

            if(chasing) {
                Chase();
            }

            
        } else {
            Debug.Log("Do Patrol");
            patrol.DoPatrol();
        }
        

        if(GameManager.instance.collectiblesCollected == 3) {
            agent.speed = 8f;
        }
    }

    public void RotateToPlayer() {
        //makes the variable target to the player position and sets the y cordinate to 0
        Vector3 target = player.transform.position;
        target.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(target - this.transform.position);

        //if time < 1 then turn smoothly towards the target
        if(curTurnTime < 1) {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, curTurnTime);

            curTurnTime += Time.deltaTime * turnSpeed;
        } else {
            curTurnTime = 0;
        }

    }

    private void Chase() {
        agent.SetDestination(player.transform.position);
    }

    public void PlayRandomSound() {
        Debug.Log("PLay Random Sound");
        int randomNum = Mathf.RoundToInt(Random.Range(0f, 1f));
        int collected = GameManager.instance.collectiblesCollected;
        AudioManager audioManager = GameManager.instance.audioManager;

        if(collected == 0) {
            
            int index = Mathf.RoundToInt(Random.Range(0f, 1f));
            string curName = normalSounds[index];
            AudioClip clip = audioManager.FindClip(curName);
            this.audioSource.clip = clip;
            this.audioSource.Play();

        }
        
        else if(collected == 1) {
            int index = Mathf.RoundToInt(Random.Range(2f, 3f));
            
            if(!kledingScript.mondkapjeOp && randomNum == 1) {
                
                string curName = kapjeSounds[0];
                AudioClip clip = audioManager.FindClip(curName);
                this.audioSource.clip = clip;   

            } else if(kledingScript.jasAan && randomNum == 1) {

                string curName = jasSounds[0];
                AudioClip clip = audioManager.FindClip(curName);
                this.audioSource.clip = clip;

            } else {
                
                string curName = normalSounds[index];
                AudioClip clip = audioManager.FindClip(curName);
                this.audioSource.clip = clip;
            }

            this.audioSource.Play();
        } 
        
        else if(collected == 2) {
            int index = Mathf.RoundToInt(Random.Range(4f, 5f));

            if(!kledingScript.mondkapjeOp && randomNum == 1) {

                string curName = kapjeSounds[1];
                AudioClip clip = audioManager.FindClip(curName);
                this.audioSource.clip = clip;

            } else if(kledingScript.jasAan && randomNum == 1) {

                string curName = jasSounds[1];
                AudioClip clip = audioManager.FindClip(curName);
                this.audioSource.clip = clip;

            } else {

                string curName = normalSounds[index];
                AudioClip clip = audioManager.FindClip(curName);
                this.audioSource.clip = clip;

            }

            this.audioSource.Play();
        } 
        
        else if(collected >= 3) {
            int index = Mathf.RoundToInt(Random.Range(0f, 1f));
            string curName = angrySounds[index];
            AudioClip clip = audioManager.FindClip(curName);
            this.audioSource.clip = clip;
            this.audioSource.Play();
        }
    }
}

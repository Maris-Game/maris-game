using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public float visionDistance;
 
    
    private GameObject player;
    private Kleding kledingScript;
    private NavMeshAgent agent;
    private SphereCollider sc;

    public bool chase;
    public float yOffset; 
    public Vector3 rayStartPos;
    public Vector3 rayDirection;
    public float fov;

    //patrol
    [Header("Patrol")]
    public Vector3 destPoint;
    public bool doPatrol = true;
    public bool destPointSet;
    public float walkRange;

    [Header("Smooth Turning")]
    public float turnSpeed = 1f;
    private Coroutine lookCoroutine;
    private float curTurnTime = 0;


    [Header("Hearing")]
    public bool hearingPlayer = false;


    private void Start() {
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
                    //if the player doesnt have their face mask on or have their jacket on, chase the playe   
                    if(!kledingScript.mondkapjeOp && chase || kledingScript.jasAan && chase) {
                    Chase();
                    }
                    Debug.DrawLine(rayStartPos, hitInfo.transform.position, Color.red);
                } else {
                    Debug.DrawLine(rayStartPos, hitInfo.transform.position, Color.yellow);
                    
                }
                RotateToPlayer();
                
            } 
        } else {
                if(doPatrol) {
                Patrol();
                }
                Debug.DrawLine(rayStartPos, transform.position + transform.forward * visionDistance, Color.green);
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


    private void Patrol() {
        if(!doPatrol) {
            return;
        }
        //sets a random point to walk towards (to be changed in the future to set points, when the map is implemented)
        if(!destPointSet) {
            float z = Random.Range(-walkRange, walkRange);
            float x = Random.Range(-walkRange, walkRange);

            destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
            
            if(Physics.Raycast(destPoint, Vector3.down, 6)) {
                destPointSet = true;
            }

        } else { 
            agent.SetDestination(destPoint); 
            if(Vector3.Distance(transform.position, destPoint) < 20f) { destPointSet = false; }
        }
    }
    
}

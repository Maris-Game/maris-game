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


    [Header("Hearing")]
    public bool hearingPlayer = false;


    private void Start() {
        sc = GetComponent<SphereCollider>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        kledingScript = player.GetComponent<Kleding>();
    }

    private void Update() {

        
        rayStartPos = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        rayDirection = player.transform.position - this.transform.position;

        RaycastHit hitInfo;
         
        if(Vector3.Angle(rayDirection, transform.forward) < fov) {
            if(Physics.Raycast (transform.position, rayDirection, out hitInfo, visionDistance)) {
                if(hitInfo.transform.tag == "Player") {
                    if(!kledingScript.mondkapjeOp && chase || kledingScript.jasAan && chase) {
                    Chase();
                    }
                    InvokeRepeating("RotateToPlayer", 0f, 1f * turnSpeed);
                    Debug.DrawLine(rayStartPos, hitInfo.transform.position, Color.red);
                }
            } 
        } else {
                if(doPatrol) {
                Patrol();
                }
                Debug.DrawLine(rayStartPos, transform.position + transform.forward * visionDistance, Color.green);
            }
    }

    public void RotateToPlayer() {
        

        if(lookCoroutine != null) {
            StopCoroutine(lookCoroutine);
        }

        lookCoroutine = StartCoroutine(LookAt());
    }

    private IEnumerator LookAt() {
        Vector3 target = player.transform.position;
        target.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(target - this.transform.position);
        

        float time = 0;

        while (time < 1) {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * turnSpeed;

            yield return null;
        }
    }

    private void Chase() {
        agent.SetDestination(player.transform.position);
    }


    private void Patrol() {
        if(!doPatrol) {
            return;
        }

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

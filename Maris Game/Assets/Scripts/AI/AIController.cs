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

    //patrol
    [Header("Patrol")]
    public Vector3 destPoint;
    public bool doPatrol = true;
    public bool destPointSet;
    public float walkRange;

    [Header("Hearing")]
    public float hearRange;
    public LayerMask playerLayer;
    public bool hearingPlayer = false;


    private void Start() {
        sc = GetComponent<SphereCollider>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        kledingScript = player.GetComponent<Kleding>();
    }

    private void Update() {
        


        RaycastHit hitInfo; 
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, visionDistance)) {
            if(hitInfo.collider.tag == "Player") {
                
                if(!kledingScript.mondkapjeOp && chase || kledingScript.jasAan && chase) {
                    Chase();
                    RotateToPlayer();
                }
                Debug.DrawLine(transform.position, hitInfo.transform.position, Color.red);
            }
        } else {
            if(doPatrol) {
                Patrol();
            }
            Debug.DrawLine(transform.position, transform.position + transform.forward * visionDistance, Color.green);
        }

        if(hearingPlayer) {
            RotateToPlayer();
        }
        
    }

    private void RotateToPlayer() {
        Vector3 target = player.transform.position;
        target.y = 0;
        transform.LookAt(target, Vector3.up);
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
            destPointSet = true;
        } else { 
            agent.SetDestination(destPoint); 
            if(Vector3.Distance(transform.position, destPoint) < 10) { destPointSet = false; }
        }
    }
    
}

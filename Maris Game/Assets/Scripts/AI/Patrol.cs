using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private AIController AIcontroller;

    [Header("Waypoints")]
    public Transform[] waypoints;
    public Transform[] waypointsLokaal33;
    public Transform waypointOutLokaal33;
    public Transform[] waypointsLokaal30;
    public Transform waypointOutLokaal30;
    
    [Header("Patrol Settings")]
    public Vector3 target;
    public bool destPointSet;
    public int wayPointNum;
    public bool doPatrol;
    public float checkRange = 2f;
    
    [Header("Lokaal Settings")]
    public float chanceOutOfLokaal;
    public bool inLokaal33;
    public bool inLokaal30;

    private void Start() {
        AIcontroller = GetComponent<AIController>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void DoPatrol() {
        if(!doPatrol) {
            return;
        }

        float dist = Vector3.Distance(transform.position, target);
        //chooses a random waypoint

        if(inLokaal33) {
            DoLokaalPatrol();
            return;
        }

        if(!destPointSet) {
            wayPointNum = Random.Range(0, waypoints.Length);
            target = waypoints[wayPointNum].position;
            if(Vector3.Distance(transform.position, target) > checkRange) {
                destPointSet = true;
                AIcontroller.walking = true;
            }
        } else { 
            agent.SetDestination(target); 
            if(Vector3.Distance(transform.position, target) < checkRange) { 
                destPointSet = false; 
                AIcontroller.walking = false;
            }
        }
    }

    public void DoLokaalPatrol() {
        if(inLokaal33) {
            
            if(!destPointSet) {
                int stayInLokaal = Mathf.RoundToInt(Random.Range(0, chanceOutOfLokaal));
                if(stayInLokaal < (chanceOutOfLokaal / 2)) {
                    wayPointNum = Random.Range(0, waypointsLokaal33.Length);
                    target = waypointsLokaal33[wayPointNum].position;
                } else { target = waypointOutLokaal33.position; }
                if(Vector3.Distance(transform.position, target) > checkRange) {
                    destPointSet = true;
                    AIcontroller.walking = true;
                }
                
            } else { 
                agent.SetDestination(target); 
                if(Vector3.Distance(transform.position, target) < checkRange) { 
                    destPointSet = false; 
                    AIcontroller.walking = false;
                }
            }
       
        } else if(inLokaal30) {
            
            int stayInLokaal = Mathf.RoundToInt(Random.Range(0, chanceOutOfLokaal));
            
            if(stayInLokaal < (chanceOutOfLokaal / 2)) {
                if(!destPointSet) {
                    wayPointNum = Random.Range(0, waypointsLokaal30.Length);
                    target = waypointsLokaal30[wayPointNum].position;
                if(Vector3.Distance(transform.position, target) > checkRange) {
                    destPointSet = true;
                    AIcontroller.walking = true;
                }
                } else { 
                    agent.SetDestination(target); 
                    if(Vector3.Distance(transform.position, target) < checkRange) { 
                        destPointSet = false; 
                        AIcontroller.walking = false;
                    }
                }
            } else {
                agent.SetDestination(waypointOutLokaal30.position); 
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Lokaal33Check") {
            inLokaal33 = true;
        } else if(other.gameObject.tag == "Lokaal30Check") {
            inLokaal30 = true; 
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Lokaal33Check") {
            inLokaal33 = false;
        } else if(other.gameObject.tag == "Lokaal30Check") {
            inLokaal30 = false;
        } else {
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private AIController AIcontroller;
    public Transform[] waypoints;
    public Vector3 target;
    public bool destPointSet;
    public int wayPointNum;
    public bool doPatrol;
    public float checkRange = 2f;

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
}

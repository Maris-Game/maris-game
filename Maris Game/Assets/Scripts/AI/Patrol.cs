using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    public Transform[] waypoints;
    public Vector3 target;
    public bool destPointSet;
    public int wayPointNum;
    public bool doPatrol;
    public float checkRange = 2f;

    private void Start() {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void DoPatrol() {
        if(!doPatrol) {
            return;
        }

        float dist = Vector3.Distance(transform.position, target);
        Debug.Log(dist);
        //chooses a random waypoint
        if(!destPointSet) {
            wayPointNum = Random.Range(0, waypoints.Length);
            target = waypoints[wayPointNum].position;
            if(Vector3.Distance(transform.position, target) > checkRange) {
                destPointSet = true;
                Debug.Log("Didnt Reach waypoint");
            }
        } else { 
            agent.SetDestination(target); 
            if(Vector3.Distance(transform.position, target) < checkRange) { 
                Debug.Log("Reached waypoint");
                destPointSet = false; 
            }
        }
    }
}

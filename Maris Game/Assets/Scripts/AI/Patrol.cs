using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Patrol() {
        if(!doPatrol) {
            return;
        }
        //chooses a random waypoint
        if(!destPointSet) {
            wayPointNum = Random.Range(0, waypoints.Length);
            target = waypoints[wayPointNum].position;
            if(Vector3.Distance(transform.position, target) > 1) {
                destPointSet = true;
            }
        } else { 
            agent.SetDestination(target); 
            if(Vector3.Distance(transform.position, target) < 5f) { destPointSet = false; }
        }
    }
}

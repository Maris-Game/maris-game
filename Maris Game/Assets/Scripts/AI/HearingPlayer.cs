using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingPlayer : MonoBehaviour
{
    private AIController aiController;

    private void Awake() {
        aiController = this.transform.parent.GetComponent<AIController>();
    }

    //if the player enters the sphere collider (AKA hearing range) rotate the AI towards the player
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            aiController.RotateToPlayer();
            aiController.hearingPlayer = true;
        }
    }   

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            aiController.hearingPlayer = false;
        }
    }
}

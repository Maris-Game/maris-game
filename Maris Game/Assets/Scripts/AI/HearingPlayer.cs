using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingPlayer : MonoBehaviour
{
    private AIController aiController;

    private void Awake() {
        aiController = this.transform.parent.GetComponent<AIController>();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Collided with something");
        if(other.gameObject.tag == "Player") {
            aiController.hearingPlayer = true;
        }
    }   

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            aiController.hearingPlayer = false;
        }
    }
}

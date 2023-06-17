using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingPlayer : MonoBehaviour
{
    private AIController aiController;
    private PlayerMovement player;
    public bool playedSound = false;
    private bool curKapje;
    private bool curJas;
    private bool rotate;

    private void Awake() {
        player = FindObjectOfType<PlayerMovement>();
        aiController = this.transform.parent.GetComponent<AIController>();
    }

    private void Update() {
        if(aiController.hearingPlayer) {
            if(curKapje != player.kledingScript.mondkapjeOp || curJas != player.kledingScript.jasAan || player.walking) {
                rotate = true;
                curKapje = player.kledingScript.mondkapjeOp;
                curJas = player.kledingScript.jasAan;
                
                if(!playedSound && aiController.inSight) {
                    playedSound = true;
                    aiController.PlayRandomSound();
                }
            }
            if(rotate) {
                aiController.RotateToPlayer();
            }
        }
    }

    //if the player enters the sphere collider (AKA hearing range) rotate the AI towards the player
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {

            aiController.hearingPlayer = true;
            curKapje = player.kledingScript.mondkapjeOp;
            curJas = player.kledingScript.jasAan;

            if(aiController.inSight && !aiController.playedSound) {
                aiController.PlayRandomSound();
                aiController.playedSound = true;
            }
        }
    }   

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            rotate = false;
            aiController.playedSound = false;
            aiController.hearingPlayer = false;
        }
    }
}

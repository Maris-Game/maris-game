using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kleding : MonoBehaviour
{
    public bool mondkapjeOp = false;
    public bool jasAan = false;
    public bool jas = false;
    public bool mondkapje = true;

    
    private GameManager gameManager;
    private InputManager inputManager;
    


    private void Start() {
        gameManager = GameManager.instance;
        inputManager = GameManager.instance.inputManager;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "mondkapje") {
            mondkapjeOp = true;
            Debug.Log("MondKapje opgedaan");
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "mondkapje") {
            mondkapjeOp = false;
            Debug.Log("mondkapje af gedaan");
        }
    }
}

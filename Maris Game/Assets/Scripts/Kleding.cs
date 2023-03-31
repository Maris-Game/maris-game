using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kleding : MonoBehaviour
{
    public bool kapjeOp = false;
    public bool jasAan = false;
    public GameObject mondkapje;
    public Animator anim;
    
    private GameManager gameManager;


    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
        mondkapje.SetActive(false);

    }
    private void Update() {
        mondKapje();
        jas();
    }

    private void mondKapje() {
        if(!gameManager.mondkapje) {
            return;
        }
        if(Input.GetKeyDown(KeyCode.E)) {
            if(!kapjeOp) {
                kapjeOp = true;
                anim.Play("mondkapjeOp", 1);
            }
            else { 
                kapjeOp = false;
                anim.Play("mondkapjeAf", 1);
            }
        }
    }

    private void jas() {
        if(!gameManager.jas) {
            return;
        }
        if(Input.GetKeyDown(KeyCode.E)) {
            if(!jasAan) {
                jasAan = true;
                anim.Play("JasAan", 1);
            }
            else { 
                jasAan = false;
                anim.Play("JasAf", 1);
            }
        }
    }


}

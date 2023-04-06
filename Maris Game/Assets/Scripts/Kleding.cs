using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kleding : MonoBehaviour
{
    public bool kapjeOp = false;
    public bool jasAan = false;
    public bool jas = false;
    public bool mondkapje = true;
    public Animator anim;
    
    private GameManager gameManager;
    private InputManager inputManager;
    


    private void Start() {
        gameManager = GameManager.instance;
        inputManager = GameManager.instance.inputManager;
    }
    private void Update() {
        MondKapje();
        Jas();

        if(Input.GetKeyDown(inputManager.switchClothKey)) {
            if(mondkapje) {
                mondkapje = false;
                jas = true;
            } else {
                mondkapje = true;
                jas = false;
            }
        }
    }

    private void MondKapje() {
        if(!mondkapje) {
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

    private void Jas() {
        if(!jas) {
            return;
        }
        if(Input.GetKeyDown(KeyCode.E)) {
            if(!jasAan) {
                jasAan = true;
                anim.Play("JasAan", 2);
            }
            else { 
                jasAan = false;
                anim.Play("JasAf", 2);
            }
        }
    }


}

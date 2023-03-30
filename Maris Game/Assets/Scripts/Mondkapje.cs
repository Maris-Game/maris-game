using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mondkapje : MonoBehaviour
{
    public bool kapjeOp = false;
    public GameObject mondkapje;
    public Animator anim;


    private void Start() {
        mondkapje.SetActive(false); 
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.E)) {
            if(!kapjeOp) {
                mondkapje.SetActive(true); 
                kapjeOp = true;
                anim.Play("mondpakjeOp");
                 
            }
            else {
                mondkapje.SetActive(false); 
                kapjeOp = false;
                anim.Play("MondkapjeAf");
            }
        }
    }


}

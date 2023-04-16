using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour
{
    public Camera cam;
    public float range;
    public Interactable interactable;
    public TextMeshProUGUI text;

    private void Awake() {
        text.gameObject.SetActive(false);
    }

    private void Update() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, range)) {
            Debug.DrawLine(transform.position, hitInfo.transform.position, Color.red);
            interactable = hitInfo.collider.GetComponent<Interactable>(); 
        }
        else {
            Debug.DrawLine(transform.position, transform.position + transform.forward * range, Color.green);
            text.gameObject.SetActive(false);
        }

        if(interactable != null) {
            if(interactable.interactSort[interactable.arrayIndex] == "Collectible") {
                text.text = "Press Q to interact";
                text.gameObject.SetActive(true);
            }
                
            if(Input.GetKeyDown(KeyCode.Q)) {
                if(interactable.interactSort[interactable.arrayIndex] == "Collectible") {
                    Debug.Log("Collected");
                    interactable.Interacted();
                    interactable = null;
                }
            }
        }
    }
}

        

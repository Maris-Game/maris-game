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
    public TextMeshProUGUI collectibleText;
    public TextMeshProUGUI bombText;

    private void Awake() {
        collectibleText.gameObject.SetActive(false);
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
            
        }

        if(interactable != null) {
            if(interactable.interactSort[interactable.arrayIndex] == "Collectible") {
                collectibleText.text = "Press Q to interact";
                collectibleText.gameObject.SetActive(true);
            } else if(interactable.interactSort[interactable.arrayIndex] == "Bomb") {
                if(GameManager.instance.canMakeBomb) {
                    bombText.text = "Press Q to make bomb";
                } else {
                    bombText.text = "Still need " + (3 - GameManager.instance.collectiblesCollected) + " objects";
                }
                bombText.gameObject.SetActive(true);
            }
                
            if(Input.GetKeyDown(KeyCode.Q)) {
                if(interactable.interactSort[interactable.arrayIndex] == "Collectible") {
                    Debug.Log("Collected");
                    interactable.Interacted();
                    interactable = null;
                }
                else if(interactable.interactSort[interactable.arrayIndex] == "Bomb") {
                    Debug.Log("Made Bomb");
                    if(GameManager.instance.canMakeBomb) {
                        interactable.Interacted();
                        GameManager.instance.Win();
                        bombText.gameObject.SetActive(false);
                        interactable = null;
                    }
                    
                }
            }
        }

        else {
            collectibleText.gameObject.SetActive(false);
            bombText.gameObject.SetActive(false);
        }
    }
}

        

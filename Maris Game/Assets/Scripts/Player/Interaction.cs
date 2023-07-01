using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour, IDataPersistence
{
    public Camera cam;
    public float range;
    public Interactable interactable;
    public TextMeshProUGUI collectibleText;
    public TextMeshProUGUI bombText;
    public TextMeshProUGUI doorText;
    public PlayerMovement playerMovement;

    public bool english;
    public bool dutch;

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
                
                if(english) {
                    collectibleText.text = "Press " + GameManager.instance.inputManager.interactKey.ToString() + " to collect";
                } else if(dutch) {
                    collectibleText.text = "Klik " + GameManager.instance.inputManager.interactKey.ToString() + " om op te pakken";
                }
                collectibleText.gameObject.SetActive(true);

            } else if(interactable.interactSort[interactable.arrayIndex] == "Bomb") {
                if(GameManager.instance.canMakeBomb) {
                    if(english) {
                        bombText.text = "Press " + GameManager.instance.inputManager.interactKey.ToString() + " to make bomb";
                    } else if(dutch) {
                        bombText.text = "Klik " + GameManager.instance.inputManager.interactKey.ToString() + " om bom te maken";
                    }
                    
                } else {
                    if(english) {
                        bombText.text = "Still need " + (3 - GameManager.instance.collectiblesCollected) + " objects";
                    } else if(dutch) {
                        bombText.text = "Nog " + (3 - GameManager.instance.collectiblesCollected) + " objecten nodig";
                    }
                    
                }
                bombText.gameObject.SetActive(true);

            } else if(interactable.interactSort[interactable.arrayIndex] == "Door") {
                if(!interactable.gameObject.GetComponent<Door>().opened) {
                    if(english) {
                        doorText.text = "Press " + GameManager.instance.inputManager.interactKey.ToString() + " to open door";
                    } else if(dutch) {
                        doorText.text = "Klik " + GameManager.instance.inputManager.interactKey.ToString() + " om deur te openen";
                    }
                }
            }
                
            if(Input.GetKeyDown(GameManager.instance.inputManager.interactKey)) {
                interactable.Interacted();
                if(interactable.interactSort[interactable.arrayIndex] == "Collectible") {
                    Debug.Log("Collected");
                }
                else if(interactable.interactSort[interactable.arrayIndex] == "Bomb") {
                    Debug.Log("Made Bomb");
                    if(GameManager.instance.canMakeBomb) {
                        playerMovement.StartCoroutine("Win");
                        playerMovement.cam.GetComponent<MouseLook>().Win();
                        bombText.gameObject.SetActive(false);
                    }
                }


                interactable = null; 
            }
        }

        else {
            collectibleText.gameObject.SetActive(false);
            bombText.gameObject.SetActive(false);
        }
    }

    public void LoadData(GameData data) {
        english = data.english;
        dutch = data.dutch;
    }

    public void SaveData(ref GameData data) {

    }
}

        

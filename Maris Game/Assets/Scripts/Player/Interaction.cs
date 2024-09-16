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
                
                if(GameManager.instance.language == "English") {
                    collectibleText.text = "Press " + GameManager.instance.inputManager.interactKey.ToString() + " to collect";
                } else if(GameManager.instance.language == "Dutch") {
                    collectibleText.text = "Klik " + GameManager.instance.inputManager.interactKey.ToString() + " om op te pakken";
                }
                collectibleText.gameObject.SetActive(true);

            } else if(interactable.interactSort[interactable.arrayIndex] == "Bomb") {
                if(GameManager.instance.canMakeBomb) {
                    if(GameManager.instance.language == "English") {
                        bombText.text = "Press " + GameManager.instance.inputManager.interactKey.ToString() + " to make bomb";
                    } else if(GameManager.instance.language == "Dutch") {
                        bombText.text = "Klik " + GameManager.instance.inputManager.interactKey.ToString() + " om bom te maken";
                    }
                    
                } else {
                    if(GameManager.instance.language == "English") {
                        bombText.text = "Still need " + (3 - GameManager.instance.collectiblesCollected) + " objects";
                    } else if(GameManager.instance.language == "Dutch") {
                        bombText.text = "Nog " + (3 - GameManager.instance.collectiblesCollected) + " objecten nodig";
                    }
                    
                }
                bombText.gameObject.SetActive(true);

            } else if(interactable.interactSort[interactable.arrayIndex] == "Door") {
                if(!interactable.gameObject.GetComponent<Door>().opened) {
                    if(GameManager.instance.language == "English") {
                        doorText.text = "Press " + GameManager.instance.inputManager.interactKey.ToString() + " to open door";
                    } else if(GameManager.instance.language == "Dutch") {
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
                        playerMovement.mainCam.GetComponent<MouseLook>().Win();
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
    }

    public void SaveData(ref GameData data) {

    }
}

        

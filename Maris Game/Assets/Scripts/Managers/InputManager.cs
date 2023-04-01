using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Controls")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode pauseKey = KeyCode.Escape;

    public GameObject inputMenu;
    public GameObject mainMenu;
    public bool askingInput;
    public string controlName;

    private void Update() {
        if(askingInput && inputMenu != null) {
            if(Input.anyKeyDown) {
                foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(kcode)) {
                        askingInput = false;
                        inputMenu.SetActive(false);
                        mainMenu.SetActive(true);
                        Debug.Log("KeyCode down: " + kcode);

                        if(controlName == "pause") {
                            pauseKey = kcode;
                        } else if(controlName == "sprint") {
                            sprintKey = kcode;
                        }
                    }
    
                }
            }
        }
    }

    public void askInput(string name) {
        askingInput = true;
        controlName = name;
        inputMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
}

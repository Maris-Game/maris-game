using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool canOpen;
    public bool opened;

    public void Interacted() {
        if(!canOpen) {
            return;
        }

        
    }
}

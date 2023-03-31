using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool jas = false;
    public bool mondkapje = true;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            if(mondkapje) {
                mondkapje = false;
                jas = true;
            } else {
                mondkapje = true;
                jas = false;
            }
        }
    }
}

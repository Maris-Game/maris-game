using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LocalManager : MonoBehaviour
{
    public TextMeshProUGUI collectiblesCollectedText;

    void Update() {
        collectiblesCollectedText.text = "Objects Collected " + GameManager.instance.collectiblesCollected + "/3";
    }

    public void ReturnToMenu() {
        GameManager.instance.ReturnToMenu();
    }

}

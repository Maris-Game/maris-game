using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalManager : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnSceneLoaded();
    }
}

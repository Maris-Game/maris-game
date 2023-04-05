using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalManager : MonoBehaviour
{
    public GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnSceneLoaded();
    }
}

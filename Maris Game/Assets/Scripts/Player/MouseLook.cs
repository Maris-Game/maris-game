using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    private float xRot;
    private float yRot;
    private InputManager inputManager;
    private GameManager gameManager;
    public bool canMove = true;
    public Camera mainCam;
    public Camera cutSceneCam;


    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        gameManager = GameManager.instance;
        inputManager = GameManager.instance.inputManager;
    }

    private void LateUpdate() {
        if(!canMove) {
            return;
        }

        float mouseX = Input.GetAxisRaw("Mouse X") * inputManager.sensX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * inputManager.sensY * Time.deltaTime;

        yRot += mouseX;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRot, yRot, 0f);
        playerBody.rotation = Quaternion.Euler(0f, yRot, 0f);
    }

    public void Win() {
        mainCam.gameObject.SetActive(false);
        cutSceneCam.enabled = true;
        canMove = false;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void GameOver() {
        mainCam.gameObject.SetActive(false);
        cutSceneCam.enabled = true;
        canMove = false;
    }
}

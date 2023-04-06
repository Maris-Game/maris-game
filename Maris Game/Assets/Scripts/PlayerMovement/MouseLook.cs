using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;

    private float xRot;
    private InputManager inputManager;
    private GameManager gameManager;


    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        gameManager = GameManager.instance;
        inputManager = GameManager.instance.inputManager;
    }

    private void Update() {
        float mouseX = Input.GetAxis("Mouse X") * inputManager.sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * inputManager.sensY * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

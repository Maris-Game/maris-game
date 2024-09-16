using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] MouseLook mouseScript;
    [SerializeField] Transform cameraPos;

    private void LateUpdate() {
        this.transform.position = cameraPos.position;
    }
}



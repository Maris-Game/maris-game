using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mondkapje : MonoBehaviour
{
    public Vector3 originalpos;
    public Quaternion originalrot;

    public void Awake() {
        originalpos = transform.position;
        originalrot = transform.rotation;
    }

    public void OnRelease() {
        transform.position = originalpos;
        transform.rotation = originalrot;
    }
}

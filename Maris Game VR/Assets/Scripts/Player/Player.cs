using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private AudioSource audioSource;
    public InputActionProperty locomotion;
    public bool walking;
    public Kleding kledingScript;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        Vector2 locoValue = locomotion.action.ReadValue<Vector2>();

        if(locoValue != Vector2.zero) {
            walking = true;
        } else {
            walking = false;
        }

        if(walking && !audioSource.isPlaying) {
            audioSource.Play();
        } else {
            audioSource.Stop();
        }
    }
}

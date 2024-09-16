using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "AI") {
            other.gameObject.GetComponent<AIController>().StartCoroutine("GameOver");
            this.gameObject.GetComponent<PlayerMovement>().StartCoroutine("GameOver");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        Debug.Log("Collided with smth");
        if(other.gameObject.tag == "AI") {
            Debug.Log("Game Over");
            GameManager.instance.GameOver();
        }
    }
}

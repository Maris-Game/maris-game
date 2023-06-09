using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light _light;

    public float minTime;
    public float maxTime;
    public float timer;


    void Start() {
        light = GetComponent<Light>();
        timer = Random.Range(minTime, maxTime);
    }
    // Update is called once per frame
    void Update()
    {
        FlickerLight();
    }

    private void FlickerLight() {
        if(timer > 0) {
            timer -= Time.deltaTime;
        }

        if(timer <=0) {
            light.enabled = false;
            timer = Random.Range(minTime, maxTime);
        }
    }
}

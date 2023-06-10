using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
	public Light light;
	public float minWaitTime;
	public float maxWaitTime;
	public bool flash;
	
	void Start () {
		light = GetComponent<Light>();
		StartCoroutine(Flashing());
	}
	
	void Update() {
		if(GameManager.instance.collectiblesCollected == 3) {
			flash = true;
		} else {
			flash = false;
		}
	}
	IEnumerator Flashing ()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minWaitTime,maxWaitTime));
			light.enabled = ! light.enabled;
		}
	}
}

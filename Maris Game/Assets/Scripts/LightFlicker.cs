using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
	public Light light;
	public float minWaitTime = 0.1f;
	public float maxWaitTime = 0.5f;

	public float[] minWaitTimes = {2f, 1f, 0.5f, 0.1f};
	public float[] maxWaitTimes = {3f, 2f, 1f, 0.5f};
	public bool flash;
	
	void Start () {
		light = GetComponent<Light>();
		StartCoroutine(Flashing());
	}
	
	void Update() {
		if(GameManager.instance.collectiblesCollected <= 3) {
			minWaitTime = minWaitTimes[GameManager.instance.collectiblesCollected];
			maxWaitTime = maxWaitTimes[GameManager.instance.collectiblesCollected];
		} else {
			minWaitTime = minWaitTimes[3];
			maxWaitTime = maxWaitTimes[3];
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

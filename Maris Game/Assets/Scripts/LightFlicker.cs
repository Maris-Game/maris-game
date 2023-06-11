using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LightFlicker : MonoBehaviour
{
	public AudioSource audioSource;
	public Light light;
	public float minWaitTime = 0.1f;
	public float maxWaitTime = 0.5f;

	public float[] minWaitTimes = {2f, 1f, 0.5f, 0.1f};
	public float[] maxWaitTimes = {3f, 2f, 1f, 0.5f};
	public bool flash;
	
	void Start () {
		audioSource = GetComponent<AudioSource>();
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

		if(audioSource != null) {
			int collected = GameManager.instance.collectiblesCollected;
			AudioManager audioManager = GameManager.instance.audioManager;

			if(collected == 0) {
				AudioClip clip = audioManager.FindClip("Lights1");
				audioSource.Play();	
			} else if(collected == 1) {
				AudioClip clip = audioManager.FindClip("Lights2");
				audioSource.Play();	
			} else if(collected == 2) {
				AudioClip clip = audioManager.FindClip("Lights3");
				audioSource.Play();	
			} else if(collected >= 3) {
				AudioClip clip = audioManager.FindClip("Lights4");
				audioSource.Play();	
			}
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

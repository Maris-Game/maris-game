using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LightFlicker : MonoBehaviour
{
	public AudioSource audioSource;
	public Light curLight;
	public float minWaitTime = 0.1f;
	public float maxWaitTime = 0.5f;
	public Material[] curMats;
	public float[] minWaitTimes = {2f, 1f, 0.5f, 0.1f};
	public float[] maxWaitTimes = {3f, 2f, 1f, 0.5f};
	public bool flash;
	public bool playingSound;
	public Material lightOnMat;
	public Material lightOfMat;
	public Renderer renderer;
	private int collected;
	
	void Start () {
		renderer = GetComponentInParent<Renderer>();
		audioSource = GetComponent<AudioSource>();
		curLight = GetComponent<Light>();
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
			AudioManager audioManager = GameManager.instance.audioManager;
			if(!audioSource.isPlaying) {
				collected = GameManager.instance.collectiblesCollected;
				if(collected == 0) {
					AudioClip clip = audioManager.FindClip("Lights1");
					audioSource.clip = clip;
					audioSource.Play();	
				} else if(collected == 1) {
					AudioClip clip = audioManager.FindClip("Lights2");
				audioSource.clip = clip;
				audioSource.Play();	
				} else if(collected == 2) {
				AudioClip clip = audioManager.FindClip("Lights3");
				audioSource.clip = clip;
				audioSource.Play();	
				} else if(collected >= 3) {
					AudioClip clip = audioManager.FindClip("Lights4");
					audioSource.clip = clip;
					audioSource.Play();	
				}
			}
			

			if(collected != GameManager.instance.collectiblesCollected) {
				audioSource.Stop();
			}	
		}

		curMats = renderer.materials;
		if(curLight.enabled) {
			if(renderer.materials[0] == lightOfMat) {
				//curMats[0] = lightOnMat;
				//renderer.materials = curMats;
			}
		} else if(!curLight.enabled) {
			if(renderer.materials[0] == lightOnMat) {
				//curMats[0] = lightOfMat;
				//renderer.materials = curMats;
			}
		}
		
	}
	IEnumerator Flashing ()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minWaitTime,maxWaitTime));
			curLight.enabled = ! curLight.enabled;
		}
	}
}

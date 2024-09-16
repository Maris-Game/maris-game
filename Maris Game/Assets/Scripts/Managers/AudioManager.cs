using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour, IDataPersistence
{
    public bool subtitles = false;
    public Sound[] sounds;

    private void Awake() {
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
        }
    }

    private void Update() {
    }

    public void PlaySound(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s != null) {
            s.source.Play();
        }
    }

    public AudioClip FindClip(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s != null) {
            return s.source.clip;
        }
        Debug.LogError("Audio Clip: " + name + " not found");
        return null;
    }

    public SubtitleObject FindSubtitle(string name) {
        if(!subtitles) {
            return null; 
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s != null) {

            if(GameManager.instance.language == "English") {
                return s.subtitleEnglish;
            } else if(GameManager.instance.language == "Nederlands") {
                return s.subtitleDutch;
            } else {
                return null;
            }
        }
        Debug.LogError("Associated Subtitle: " + name + " not found");
        return null;
    }

    public void StopAllSounds() {
        foreach (Sound s in sounds) {
            s.source.Stop();
        }
    }

    public void LoadData(GameData data) {
        subtitles = data.subtitles;
    }

    public void SaveData(ref GameData data) {

    }

    public void OnSceneLoaded() {

    }

}

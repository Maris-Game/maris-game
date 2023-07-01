using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour, IDataPersistence
{
    public bool subtitles = false;
    public bool english = true;
    public bool dutch = false;
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

            if(english) {
                return s.subtitleEnglish;
            } else if(dutch) {
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
        english = data.english;
        dutch = data.dutch;
    }

    public void SaveData(ref GameData data) {

    }

    public void OnSceneLoaded() {

    }

}

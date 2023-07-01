using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Subtitle Object", menuName = "Asset/Subtitles")]
public class SubtitleObject : ScriptableObject
{
    public AudioClip clip;
    public string subtitle;
}

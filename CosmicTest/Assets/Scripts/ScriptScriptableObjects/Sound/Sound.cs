using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Sound", menuName = "ScriptableObjects/Sound", order = 0)]
public class Sound : ScriptableObject
{
    public string nameSong;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public AudioClip song;
    public bool loop;
    public AudioMixerGroup mixerGroup;
    [HideInInspector]
    public AudioSource source;
    public MusicLevel levelSound;
    public MusicType musicType;
}

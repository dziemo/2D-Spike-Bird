using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public Sound[] sounds;

    private void Awake()
    {
        instance = this;
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.playOnAwake = false;
            s.source.clip = s.clip;
        }
    }

    public void PlayClip (AudioType type)
    {
        Sound s = Array.Find(sounds, sound => sound.type == type);
        s.source.Play();
    }

    public void PlayButtonSound ()
    {
        Sound s = Array.Find(sounds, sound => sound.type == AudioType.Button);
        s.source.Play();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public AudioType type;
    public AudioSource source;
}

public enum AudioType { WallHit, CollectCoin, Fly, Death, Button }
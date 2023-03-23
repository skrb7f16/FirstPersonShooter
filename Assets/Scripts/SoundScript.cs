using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundScript 
{
    // Start is called before the first frame update
    public string name;
    public AudioClip clip;
    public float volume;
    public AudioSource source;
    public bool isLoop;


}

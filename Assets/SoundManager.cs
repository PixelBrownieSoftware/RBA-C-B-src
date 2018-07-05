using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour {

    public AudioSource src;
    public static SoundManager sfx;

    private void Start()
    {
        sfx = this;
        src = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        List<AudioClip> sounds = new List<AudioClip>();
        sounds.Add(sound);
        foreach (AudioClip clip in sounds) {
            src.PlayOneShot(clip);
        }
    }
}

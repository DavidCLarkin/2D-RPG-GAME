using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioClip music1;

	// Use this for initialization
	void Start ()
    {
        SoundManager.instance.musicSource.clip = music1;
        SoundManager.instance.musicSource.Play();
	}
	
	// Update is called once per frame
	void Update ()
    {
		// Later change music if boss fight etc
	}
}

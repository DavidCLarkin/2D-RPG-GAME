using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioClip music1;

	// Use this for initialization
	void Start ()
    {
        SoundManager.instance.PlayMusic(music1);
	}
	
}

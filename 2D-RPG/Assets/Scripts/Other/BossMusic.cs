using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    public AudioClip bossMusic;
    private bool hasBossMusicStarted = false;

    private void Update()
    {
        // Decide to play the boss music or not
        if(Vector2.Distance(GameManagerSingleton.instance.player.transform.position, gameObject.transform.position) <= GetComponent<Enemy>().followRange)
        {
            if (!hasBossMusicStarted)
            {
                SoundManager.instance.musicSource.clip = bossMusic;
                StartCoroutine(SoundManager.instance.FadeIn(SoundManager.instance.musicSource, 5f));
                hasBossMusicStarted = true;
            }
        }
        else
        {
            SoundManager.instance.musicSource.Stop();
            hasBossMusicStarted = false;
        }

    }
}

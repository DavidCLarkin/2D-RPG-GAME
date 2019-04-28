using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusicHandler : MonoBehaviour
{
    public GameObject boss;
    private bool hasBossMusicStarted = false;

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag(GameManagerSingleton.instance.bossTag);    
    }

    private void Update()
    {
        if(!boss)
            boss = GameObject.FindGameObjectWithTag(GameManagerSingleton.instance.bossTag);

        HandleMusic();
    }

    /*
     * Decide whether to player the boss music according to distance
     */ 
    void HandleMusic()
    {
        if (boss != null)
        {
            if (Vector2.Distance(boss.transform.position, GameManagerSingleton.instance.player.transform.position) < 12f)
            {
                if (!hasBossMusicStarted)
                {
                    SoundManager.instance.musicSource.clip = boss.GetComponent<BossMusic>().bossMusic;
                    StartCoroutine(SoundManager.instance.FadeIn(SoundManager.instance.musicSource, 5f));
                    hasBossMusicStarted = true;

                }
            }
        }
        else
        {
            SoundManager.instance.musicSource.Stop();
        }
    }

    
}

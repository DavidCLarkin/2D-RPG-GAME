using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource sfxSource;            
    public AudioSource musicSource;                
    public static SoundManager instance = null;

    // General
    public AudioClip[] equipWeaponSounds;
    public AudioClip[] attackWeaponSounds;
    public AudioClip[] pickUpItemSounds;
    public AudioClip[] dropItemSounds;
    public AudioClip[] pageSounds;

    // General Enemy
    public AudioClip spawnTileSound;

    // Wizard
    public AudioClip[] wizardSpellProjectileSounds;
    public AudioClip[] wizardSpawnMinionSounds;
    public AudioClip[] wizardMinionWhooshSounds;

    // Demon
    public AudioClip[] demonProjectileSounds;
    public AudioClip[] demonFireSounds;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    //One shot to play multiple sounds at once
    public void PlayOneShot(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.PlayOneShot(clip);
    }

    // Play a random sound given a list of clips
    public void PlayRandomOneShot(params AudioClip[] clips)
    {
        int randomSound = Random.Range(0, clips.Length);
        sfxSource.clip = clips[randomSound];
        sfxSource.PlayOneShot(clips[randomSound]);
    }

    // Play a random sound given a list of clips at a volume level
    public void PlayRandomOneShot(float volumeLevel, params AudioClip[] clips)
    {
        int randomSound = Random.Range(0, clips.Length);
        sfxSource.clip = clips[randomSound];
        sfxSource.PlayOneShot(clips[randomSound], volumeLevel);
    }

    // Play a random sound at a certain point in the world space
    public void PlayRandomOneShotAtPoint(float volumeLevel, Vector3 position, params AudioClip[] clips)
    {
        int randomSound = Random.Range(0, clips.Length);
        AudioSource.PlayClipAtPoint(clips[randomSound], position, volumeLevel);
    }

    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }


    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        sfxSource.clip = clips[randomIndex];
        sfxSource.Play();
    }

    public IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        audioSource.volume = 0;
        audioSource.Play();

        while(audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.volume = startVolume;

    }
}

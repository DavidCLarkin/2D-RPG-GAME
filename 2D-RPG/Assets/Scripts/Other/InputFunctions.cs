using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InputFunctions : MonoBehaviour
{
    public string sceneToLoad;
    public string tutorialScene;
    public AudioClip menuMusic;

    private void Start()
    {
        SoundManager.instance.musicSource.clip = menuMusic;
        SoundManager.instance.musicSource.Play();
    }

    private void LoadScene()
    {
        PlayerData data = SaveSystem.Load();
        if (data == null || !data.hasCompletedTutorial)
            SceneManager.LoadScene(tutorialScene);
        else if(data.hasCompletedTutorial)
            SceneManager.LoadScene(sceneToLoad);

    }

    private void Update()
    {
        if(Input.anyKey)
        {
            LoadScene();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevelByString : MonoBehaviour
{
    public string levelToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == GameManagerSingleton.instance.playerColliderTag)
        {
            // if finishing tutorial, then load and save game
            if (tag == "EndTutorial")
            {
                GameManagerSingleton.instance.hasCompletedTutorial = true;
                SceneManager.LoadScene(levelToLoad);
                // initial save
                SaveSystem.Save(GameManagerSingleton.instance.player);
            }
            else
                SceneManager.LoadScene(levelToLoad); // just load normally

        }
    }
}

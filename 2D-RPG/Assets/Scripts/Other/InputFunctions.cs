using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InputFunctions : MonoBehaviour
{
    public string sceneToLoad;

    private void LoadScene()
    {
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

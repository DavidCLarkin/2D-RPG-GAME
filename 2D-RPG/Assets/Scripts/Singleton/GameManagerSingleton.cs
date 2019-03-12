using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class GameManagerSingleton : MonoBehaviour 
{
	public static GameManagerSingleton instance;

	//Objects
	public Text tooltip;
	public GameObject player;

    public bool isPaused;
    private InputComponent input;

    private void Awake()
	{
		if (instance != null)
		{
			Destroy (gameObject);
		} 
		else
		{
            player = GameObject.FindGameObjectWithTag("Player");
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

    private void Start()
    {
        input = player.GetComponent<InputComponent>();
        input.OnPause += PauseGame;
    }

    private void Update()
    {
        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    
    public void PauseGame()
    {
        isPaused = !isPaused;
    }

}

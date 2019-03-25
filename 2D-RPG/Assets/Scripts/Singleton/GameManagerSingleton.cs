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
    public GameObject pausePanel;
    private StatVendor statVendor;

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
        statVendor = GameObject.Find("Stat Vendor NPC").GetComponent<StatVendor>();
        //pausePanel = GameObject.Find("PausePanel");
    }

    private void Update()
    {
        HandlePause();
    }
    
    void HandlePause()
    {
        //if (statVendor.panelOpen) return;

    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

}

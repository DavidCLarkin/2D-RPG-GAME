using System.Collections;
using UnityEngine.SceneManagement;
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
    public GameObject controlsPanel;

    [HideInInspector]
    public StatVendor statVendor;

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
        StartCoroutine(UpdateUI());
        //pausePanel = GameObject.Find("PausePanel");
    }

    private void Update()
    {
        HandlePause();

        //Time.timeScale = isPaused ? 0 : 1;

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

    //Player death, slow time and display on screen text, then reload
    public IEnumerator PlayerDeath(float delay)
    {
        Time.timeScale = 0.5f;
        player.SetActive(false);

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene("Hub");
        player.GetComponent<Player>().Load();

        Time.timeScale = 1;
        player.SetActive(true);

    }

    public void OpenControlsPanel()
    {
        controlsPanel.SetActive(!controlsPanel.activeSelf);

    }

    // Coroutine to update the UI every 1/10th second instead of every frame
    public IEnumerator UpdateUI()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            player.GetComponent<Stats>().StatUIUpdate();
        }
    }
}

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

    //[HideInInspector]
    public bool hasCompletedTutorial;

    //[HideInInspector]
    public StatVendor statVendor;

    public bool isPaused;
    private InputComponent input;

    [HideInInspector]
    public string playerColliderTag = "PlayerColliders"; // for collision 
    [HideInInspector]
    public string playerTag = "Player"; // for parent object
    [HideInInspector]
    public string bossMinionTag = "BossMinion"; // used if boss dies to destroy all boss objects
    [HideInInspector]
    public string bossTag = "Boss";
    [HideInInspector]
    public string enemyTag = "Enemy";
    

    private void Awake()
	{
		if (instance != null)
		{
			Destroy (gameObject);
		} 
		else
		{
            player = GameObject.FindGameObjectWithTag(playerTag);
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
        if(statVendor != null)
        {
            if(statVendor.panelOpen)
            {
                player.GetComponent<MovementComponent>().enabled = false;
                player.GetComponent<PlayerAnimationComponent>().enabled = false;
            }
            else if(!statVendor.panelOpen)
            {
                player.GetComponent<MovementComponent>().enabled = true;
                player.GetComponent<PlayerAnimationComponent>().enabled = true;
            }
        }

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
        //player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(delay);

        player.GetComponent<SpriteRenderer>().enabled = true;

        SceneManager.LoadScene("Hub");
        GetComponent<MenuButtonFunctions>().Load();

        Time.timeScale = 1;

        //player.GetComponent<SpriteRenderer>().enabled = true;

    }

    public void OpenControlsPanel()
    {
        SoundManager.instance.PlayRandomOneShot(SoundManager.instance.pageSounds);
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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    // When a new scene is loaded, run this code
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        player.GetComponent<Inventory>().PopulateInventory();
        if(scene.name.Equals("Hub"))
            statVendor = GameObject.Find("Stat Vendor NPC").GetComponent<StatVendor>();
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    private KeyCode[] keys = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    private Rigidbody2D rigidbody;
    private Camera cam;
    private Animator anim;
    public Interactable focus;

    //private float vertical;
    //private float horizontal;

    //[SerializeField]
    //private float speed = 2.5f;

    private static bool playerExists;

    private bool walkingUp;
    private bool walkingDown;
    private bool walkingLeft;
    private bool walkingRight;
    private bool attackDown;
    private bool attackUp;
    private bool attackLeft;
    private bool attackRight;
    public bool isAttacking;


    private int facingDirection;

    public float dodgeSpeed;
    public float startDodgetime;
    private float dodgeTime;
    private int direction;

    public string startPoint;

    // Use this for initialization
    void Start ()
    {
        //inventory = GetComponent<Inventory>();
        facingDirection = 3; // Set facing down
        cam = Camera.main;
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dodgeTime = startDodgetime;

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
            Destroy(gameObject);
    }

    #region Should be done in GameManager - Load/Save/Quit etc
    public void Save()
    {
        if (SceneManager.GetActiveScene().name == "Hub")
            SaveSystem.Save(gameObject);
        else
            Debug.Log("Can only save in Hub");
    }

    public void Load()
    {
        if (SceneManager.GetActiveScene().name != "Hub") // Load back to hub
            SceneManager.LoadScene("Hub");

        PlayerData data = SaveSystem.Load();

        //gameObject.GetComponent<MovementComponent>().maxStamina = data.maxStamina;
        //gameObject.GetComponent<HealthComponent>().maxHealth = data.maxHealth;
        gameObject.GetComponent<Stats>().HealthStat.StatLevel = data.healthLevel;
        gameObject.GetComponent<Stats>().StaminaStat.StatLevel = data.staminaLevel;
        gameObject.GetComponent<ExperienceComponent>().totalExp = data.totalExp;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        gameObject.GetComponent<Stats>().UpdateVariables();
        //GameManagerSingleton.instance.isPaused = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowControls()
    {
        GameManagerSingleton.instance.OpenControlsPanel();
    }

    #endregion

}

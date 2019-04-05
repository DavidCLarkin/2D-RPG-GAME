using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevel : Interactable
{
	public string levelToLoad;
	public string exitPoint;
    public string textToDisplay;

	private Player playerComponent;

	// Use this for initialization
	void Start () 
	{
        player = GameManagerSingleton.instance.player.transform; 
        playerComponent = player.GetComponent<Player>();
	}

    public override void Update()
    {
        float distance = Vector2.Distance(player.position, transform.position);

        if (isFocus && !hasInteracted)
        {
            if (distance <= radius)
            {
                Interact();
            }
        }

    }

    public override void Interact()
    {
        if(player.GetComponent<InputComponent>().Interact)
        {
            Destroy(gameObject);
            hasInteracted = true;
            SceneManager.LoadScene(levelToLoad);
            playerComponent.startPoint = exitPoint;
            DisableTooltip();
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
	{
        GameManagerSingleton.instance.tooltip.text = textToDisplay;
        EnableTooltip();
	}
    
}

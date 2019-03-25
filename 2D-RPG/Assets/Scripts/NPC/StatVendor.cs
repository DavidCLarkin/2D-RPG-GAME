using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatVendor : Interactable
{
	public GameObject upgradePanel;
	public bool panelOpen = false;

	// Use this for initialization
	void Start ()
    {
        player = GameManagerSingleton.instance.player.transform;
		upgradePanel = GameObject.Find("Canvas").transform.Find("StatsUpgrade").gameObject;
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

        upgradePanel.SetActive(panelOpen);
    }

    public override void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            panelOpen = !panelOpen;
            hasInteracted = true;
            Time.timeScale = panelOpen ? 0 : 1; // if panel open, pause
        }
        
        //GameManagerSingleton.instance.PauseGame();
    }

    protected override void EnableTooltip()
    {
        GameManagerSingleton.instance.tooltip.text = gameObject.name;
        base.EnableTooltip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnableTooltip();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DisableTooltip();
        ClosePanel(); // out of range
    }

    void OnMouseEnter()
    {
        EnableTooltip();
    }

    void OnMouseExit()
    {
        DisableTooltip();
    }

    public void ClosePanel()
    {
        if (panelOpen)
            panelOpen = false;
    }
}

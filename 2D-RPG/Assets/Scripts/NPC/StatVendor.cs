using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatVendor : Interactable
{
	public GameObject upgradePanel;
	bool panelOpen = false;

	// Use this for initialization
	void Start ()
    {
        player = GameManagerSingleton.instance.player.transform;
		upgradePanel = GameObject.Find("Canvas").transform.Find("StatsUpgrade").gameObject;
	}

	void Update()
	{
		upgradePanel.SetActive (panelOpen);
	}

    public override void Interact()
    {
        Debug.Log("Show GUI");
		panelOpen = true;

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
		panelOpen = false;
    }

    void OnMouseEnter()
    {
        EnableTooltip();
    }

    void OnMouseExit()
    {
        DisableTooltip();
    }
}

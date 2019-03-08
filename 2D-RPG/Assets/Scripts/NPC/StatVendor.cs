using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatVendor : Interactable
{

	// Use this for initialization
	void Start ()
    {
        player = GameManagerSingleton.instance.player.transform;
	}

    public override void Interact()
    {
        Debug.Log("Show GUI");
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

using UnityEngine;

public enum TYPE { Consumable, Weapon, Note };

public abstract class Item : Interactable 
{
    public TYPE type;
    public string itemName;
    [TextArea]
    public string description;
    public int itemID;
    public Sprite icon;

    public override void Update () 
	{
		base.Update();
	}

    public virtual void Use()
    {
        // To be overridden
    }

    /*
     * Overriding base class Interact method to pick up an item
     */ 
	public override void Interact()
	{
        //base.Interact();
        if (type == TYPE.Consumable || type == TYPE.Weapon)
        {
            Debug.Log("Added to items");
            PickUp();
        }
        else if(type == TYPE.Note)
        {
            Debug.Log("Reading Note etc");
            PickUp();
        }

	}

    /*
     * Method to add an item to the Inventory and disable tooltip afterwards
     */ 
	void PickUp()
	{
		Inventory.instance.Add(this);
        if (GameManagerSingleton.instance.tooltip.gameObject.activeSelf)
            GameManagerSingleton.instance.tooltip.gameObject.SetActive(false);

	}
    
    /*
     * Enable the tool tip. Used when close to an item (colliding via trigger)
     */ 
    protected override void EnableTooltip()
    {
        GameManagerSingleton.instance.tooltip.text = itemName;
        base.EnableTooltip();
    }

    /*
     * When colliding with object, enable tooltip
     */ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnableTooltip();
    }

    /*
     * When exiting collider, disable tooltip
     */ 
    private void OnTriggerExit2D(Collider2D collision)
    {
        DisableTooltip();
    }

    // same as on trigger
    void OnMouseEnter()
    {
        EnableTooltip();
    }

    // same as exiting trigger
    void OnMouseExit()
    {
        DisableTooltip();
    }

}

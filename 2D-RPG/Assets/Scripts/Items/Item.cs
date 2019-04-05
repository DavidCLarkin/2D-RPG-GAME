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

	void PickUp()
	{
		Inventory.instance.Add(this);
        if (GameManagerSingleton.instance.tooltip.gameObject.activeSelf)
            GameManagerSingleton.instance.tooltip.gameObject.SetActive(false);

	}

    protected override void EnableTooltip()
    {
        GameManagerSingleton.instance.tooltip.text = itemName;
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

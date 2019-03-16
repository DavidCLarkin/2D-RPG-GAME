using UnityEngine;

public enum TYPE { Consumable, Weapon, Note };

public class Item : Interactable 
{
    public TYPE type;
    public string itemName;
    public string description;
    public Sprite icon;

    public override void Update () 
	{
		base.Update();
	}

    public void Use()
    {
        if(type == TYPE.Consumable)
        {
            GameManagerSingleton.instance.player.GetComponent<HealthComponent>().health += 20;
            Debug.Log("Increased Health");
        }
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

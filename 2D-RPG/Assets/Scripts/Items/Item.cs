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

    void OnMouseEnter()
    {
        GameManagerSingleton.instance.tooltip.text = itemName;
        GameManagerSingleton.instance.tooltip.gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        GameManagerSingleton.instance.tooltip.gameObject.SetActive(false);
    }

}

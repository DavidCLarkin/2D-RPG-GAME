using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour 
{
	public static Inventory instance;
    public int numberOfSlots = 4;
    public GameObject slotHolder;
    public int slotSelected = 0;
    private InputComponent input;
    private Slot[] itemSlots;

	#region Singleton
	void Awake()
	{
        /*
		if(instance != null)
		{
			Debug.Log("More than one instance of inventory found");
			return;
		}

		instance = this;
        */
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    public GameObject[] slots;

    public void Start()
    {
        input = GetComponent<InputComponent>();

        input.OnInventoryMoveLeft += ChangeSlotLeft;
        input.OnInventoryMoveRight += ChangeSlotRight;
        input.OnUseInventoryItem += UseItemSelected;
        input.OnInventoryDropItem += DropItemSelected;

        slots = new GameObject[numberOfSlots];

        for (int i = 0; i < numberOfSlots; i++)
        {
           slots[i] = slotHolder.transform.GetChild(i).gameObject;

            if (slots[i].GetComponent<Slot>().itemName == "")
                slots[i].GetComponent<Slot>().isEmpty = true;

        }

        // Get each slot in the Inventories assigned Grid
        itemSlots = slotHolder.GetComponentsInChildren<Slot>();

    }

    public void PopulateInventory()
    {
        foreach (Slot s in itemSlots)
        {
            foreach (GameObject item in GameManagerSingleton.instance.GetComponent<ItemDatabase>().items)
            {
                if (s.itemName.Equals(item.name))
                    s.item = item.GetComponent<Item>();
            }
        }
    }

    /*
     * Set the highlight image thats currently active (if there is one) inactive, and activate the next one
     */
    void ChangeSlotRight()
    {
        if (slotSelected < numberOfSlots - 1)
        {
            if (itemSlots[slotSelected].GetComponentInChildren<RectTransform>().GetChild(0).gameObject.activeSelf)
                itemSlots[slotSelected].GetComponentInChildren<RectTransform>().GetChild(0).gameObject.SetActive(false);

            slotSelected++;
            itemSlots[slotSelected].GetComponentInChildren<RectTransform>().GetChild(0).gameObject.SetActive(true);
        }
    }

    /*
    * Set the highlight image thats currently active (if there is one) inactive, and activate the next one
    */
    void ChangeSlotLeft()
    {
        if (slotSelected > 0)
        {
            if (itemSlots[slotSelected].GetComponentInChildren<RectTransform>().GetChild(0).gameObject.activeSelf)
                itemSlots[slotSelected].GetComponentInChildren<RectTransform>().GetChild(0).gameObject.SetActive(false);

            slotSelected--;
            itemSlots[slotSelected].GetComponentInChildren<RectTransform>().GetChild(0).gameObject.SetActive(true);
        }
    }

    void UseItemSelected()
    {
        if (!itemSlots[slotSelected].isEmpty)
            itemSlots[slotSelected].UseItem();
    }

    void DropItemSelected()
    {
        //Debug.Log("Drop");
        if(Time.timeScale != 0)
        {
            if (!itemSlots[slotSelected].isEmpty)
            {
                Remove(itemSlots[slotSelected].item);
                itemSlots[slotSelected].RemoveItem();
            }
        }
    }

    /*
     * Add an item to the player's inventory
     */
    public void Add(Item item)
	{
        for (int i = 0; i < numberOfSlots; i++)
        {
            if(slots[i].GetComponent<Slot>().isEmpty) //empty slot
            {
                slots[i].GetComponent<Slot>().item = item;
                slots[i].GetComponent<Slot>().itemName = item.itemName;
                slots[i].GetComponent<Slot>().description = item.description;
                slots[i].GetComponent<Slot>().icon = item.icon;
                slots[i].GetComponent<Slot>().isEmpty = false;
                item.gameObject.SetActive(false);
                GameManagerSingleton.instance.player.GetComponent<InteractComponent>().RemoveFocus();
                //Destroy(item.gameObject);

                return;
            }
        }
	}

	//public void Remove(Item item)
    public void Remove(Item item)
	{
        if (item != null)
        {
            foreach(GameObject i in GameManagerSingleton.instance.GetComponent<ItemDatabase>().items)
            {
                if(item.itemName.Equals(i.GetComponent<Item>().itemName))
                {
                    Instantiate(i, gameObject.transform.position, Quaternion.identity);
                }
            }
            /*
            //item.gameObject.transform.position = GameManagerSingleton.instance.player.transform.position;
            GameObject obj = new GameObject();
            obj.tag = "Item";
            obj.name = item.itemName;
            obj.transform.position = GameManagerSingleton.instance.player.transform.position;

            if (item.type == TYPE.Weapon)
            {
                WeaponItem weapon = (WeaponItem)item;
                WeaponItem i = obj.AddComponent<WeaponItem>();
                i.itemName = item.itemName;
                i.type = item.type;
                i.icon = item.icon;
                i.description = item.description;
                i.damage = weapon.damage;

                SpriteRenderer spr = obj.AddComponent<SpriteRenderer>();
                spr.sprite = i.icon;
                spr.sortingOrder = 5;

                obj.transform.localScale = new Vector2(3.2f, 3.2f);

                BoxCollider2D col = obj.AddComponent<BoxCollider2D>();
                col.isTrigger = true;
                col.size = col.size * 2.5f;
                
            }

            if(item.type == TYPE.Consumable)
            {
                ConsumableItem consumable = (ConsumableItem)item;
                ConsumableItem i = obj.AddComponent<ConsumableItem>();
                i.itemName = item.itemName;
                i.type = item.type;
                i.icon = item.icon;
                i.description = item.description;
                i.amount = consumable.amount;

                SpriteRenderer spr = obj.AddComponent<SpriteRenderer>();
                spr.sprite = i.icon;
                spr.sortingOrder = 5;

                obj.transform.localScale = new Vector2(3.2f, 3.2f);

                BoxCollider2D col = obj.AddComponent<BoxCollider2D>();
                col.isTrigger = true;
                col.size = col.size * 2.5f;

            }
            */


            //Instantiate(obj, GameManagerSingleton.instance.player.transform.position, Quaternion.identity);
            //item.gameObject.SetActive(true);
        }
	}

	public void ListItems()
	{
	}
}

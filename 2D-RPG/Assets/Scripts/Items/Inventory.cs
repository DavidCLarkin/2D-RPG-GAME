using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour 
{
	public static Inventory instance;
    public int numberOfSlots = 4;
    public GameObject slotHolder;

	#region Singleton
	void Awake()
	{
		if(instance != null)
		{
			Debug.Log("More than one instance of inventory found");
			return;
		}

		instance = this;
	}

    #endregion

    public GameObject[] slots;

    public void Start()
    {
        slots = new GameObject[numberOfSlots];

        for (int i = 0; i < numberOfSlots; i++)
        {
           slots[i] = slotHolder.transform.GetChild(i).gameObject;

            if (slots[i].GetComponent<Slot>().itemName == "")
                slots[i].GetComponent<Slot>().isEmpty = true;

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
                slots[i].GetComponent<Slot>().itemName = item.itemName;
                slots[i].GetComponent<Slot>().description = item.description;
                slots[i].GetComponent<Slot>().icon = item.icon;
                slots[i].GetComponent<Slot>().isEmpty = false;
                //item.gameObject.SetActive(false);
                Destroy(item.gameObject);

                return;
            }
        }
	}

	public void Remove(Item item)
	{
		//items.Remove(item);
	}

	public void ListItems()
	{
	}
}

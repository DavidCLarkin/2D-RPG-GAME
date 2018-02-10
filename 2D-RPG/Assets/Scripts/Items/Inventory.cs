using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour 
{
	public static Inventory instance;

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

	public List<Item> items = new List<Item>();

	public void Add(Item item)
	{
		items.Add(item);
	}

	public void Remove(Item item)
	{
		items.Remove(item);
	}

	public void ListItems()
	{
		foreach(Item item in items)
		{
			print(item.name);
		}
	}
}

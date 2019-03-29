using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite icon;
    public bool isEmpty;
    public Item item;

    private void Update()
    {
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        gameObject.GetComponent<Image>().sprite = icon;
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            if (item.type == TYPE.Consumable)
            {
                RemoveItemCompletely();
                UpdateSlot();
            }
        }
    }

    // For dropping items
    public void RemoveItem()
    {
        itemName = "";
        description = "";
        icon = null;
        isEmpty = true;
        Inventory.instance.Remove(item);
        item = null;
    }

    // If consumed, destroy
    public void RemoveItemCompletely()
    {
        itemName = "";
        description = "";
        icon = null;
        isEmpty = true;
        item = null;
    }
}

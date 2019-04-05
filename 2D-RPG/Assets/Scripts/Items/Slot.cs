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
    public int itemID;
    public Item item;
    public Sprite emptyImage;
    public Text itemInfo;

    private void Update()
    {
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (!isEmpty)
            gameObject.GetComponent<Image>().sprite = icon;
        else
            gameObject.GetComponent<Image>().sprite = emptyImage;
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
        itemID = 0;
    }

    // If consumed, destroy
    public void RemoveItemCompletely()
    {
        itemName = "";
        description = "";
        icon = null;
        isEmpty = true;
        item = null;
        itemID = 0;
    }

    public void DisplayItemInfo()
    {
        if (item)
        {
            if (item.type == TYPE.Weapon)
            {
                WeaponItem weapon = (WeaponItem)item;
                itemInfo.text = itemName + "\n" + "Damage: " + weapon.damage + "\n" + description;
            }
            else if (item.type == TYPE.Consumable)
            {
                ConsumableItem cons = (ConsumableItem)item;
                itemInfo.text = itemName + "\n" + description + "\n" + "Amount: " + cons.amount;
            }
            else if(item.type == TYPE.Note)
            {
                itemInfo.text = itemName;
            }
            itemInfo.gameObject.SetActive(true);
        }
    }

    public void DisableItemInfo()
    {
        itemInfo.gameObject.SetActive(false);
    }

}

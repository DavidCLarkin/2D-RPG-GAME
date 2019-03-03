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

    public void UpdateSlot()
    {
        gameObject.GetComponent<Image>().sprite = icon;
    }

    private void Update()
    {
        UpdateSlot();
    }
}

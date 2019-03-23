using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ClickableButton : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            GetComponent<Slot>().UseItem();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
            GetComponent<Slot>().RemoveItem();
    }
}

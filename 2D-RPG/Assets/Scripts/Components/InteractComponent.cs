using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractComponent : MonoBehaviour
{
    public Interactable focus;
    private Camera cam;
    // Use this for initialization
    void Start ()
    {
        cam = Camera.main;
        GetComponent<InputComponent>().OnInteract += Interact;
	}

    void Interact()
    {
        //Debug.Log(Input.GetJoystickNames().Length > 0 ? true : false);

        // Easier to do with controller, may change
        if (focus != null)
        {
            string focusTag = focus.tag;
            switch (focusTag)
            {
                case "NPC":
                    focus.Interact();
                    break;
                case "Item":
                    focus.Interact();
                    break;

            }
        }
    }

    // Set interactable focus to new one
    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
        newFocus.OnFocused(transform);
    }

    //Remove the focus
    public void RemoveFocus()
    {
        if (focus == null) return;

        focus.DeFocused();
        focus = null;
    }

    // Set focus to the object the player is near (items etc.)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Interactable>() != null)
        {
            Interactable interactable = collision.gameObject.GetComponent<Interactable>();
            SetFocus(interactable);
        }
    }

    // Remove focus on exit
    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveFocus();
    }
}

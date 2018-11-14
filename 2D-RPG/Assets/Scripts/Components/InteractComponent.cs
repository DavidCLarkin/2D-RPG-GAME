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
        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                string intTag = interactable.tag;
                switch (intTag)
                {
                    case "Enemy": // TODO add attacking, interaction etc.
                        Debug.Log("Enemy CASE"); //ATTACK
                        break;
                    case "Item":
                        Debug.Log("Item CASE"); //PICK UP
                        break;
                    default:
                        Debug.Log("Default");
                        break;
                }
                setFocus(interactable);
            }
        }
    }

    void setFocus(Interactable newFocus)
    {
        focus = newFocus;
        newFocus.onFocused(transform);
    }

    void removeFocus()
    {
        focus.deFocused();
        focus = null;
    }
}

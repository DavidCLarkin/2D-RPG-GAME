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
        /*else
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    string intTag = interactable.tag;
                    switch (intTag)
                    {
                        /*
                        case "Enemy": // TODO add attacking, interaction etc.
                            Debug.Log("Enemy CASE"); //ATTACK
                            break;
                            
                        case "NPC":
                            Debug.Log("NPC CASE");
                            interactable.Interact();
                            break;
                        case "Item":
                            Debug.Log("Item CASE"); //PICK UP
                            break;
                        default:
                            Debug.Log("Default");
                            break;
                    }
                    SetFocus(interactable);
                }
            }
        }
*/

    }

    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
        newFocus.OnFocused(transform);
    }

    public void RemoveFocus()
    {
        if (focus == null) return;

        focus.DeFocused();
        focus = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Interactable>() != null)
        {
            Interactable interactable = collision.gameObject.GetComponent<Interactable>();
            SetFocus(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveFocus();
    }
}

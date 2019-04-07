using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class TriggerHintText : MonoBehaviour
{
    public TextMeshProUGUI textToShow;
    [TextArea]
    public string info;
    private bool hasTriggered = false;

    private void Update()
    {
        if (hasTriggered)
        {
            if (Vector2.Distance(gameObject.transform.position, GameManagerSingleton.instance.player.transform.position) > 4f)
            {
                Destroy(gameObject);
                textToShow.enabled = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == GameManagerSingleton.instance.playerColliderTag)
        {
            textToShow.text = info;
            textToShow.enabled = true;
            hasTriggered = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //textToShow.enabled = false;
        //Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteItem : Item
{
    //private NoteHandler noteHandler;

    private void Start()
    {
        player = GameManagerSingleton.instance.player.transform;
    }

    /*
     * Use a note to show the page on screen so the player can read. Also play a random sound
     */ 
    public override void Use()
    {
        if (GameManagerSingleton.instance.GetComponent<NoteHandler>().isNotePanelOpen == false)
        {
            GameManagerSingleton.instance.GetComponent<NoteHandler>().ReadCurrentFile();
        }

        SoundManager.instance.PlayRandomOneShot(SoundManager.instance.pageSounds);
        GameManagerSingleton.instance.GetComponent<NoteHandler>().isNotePanelOpen = !GameManagerSingleton.instance.GetComponent<NoteHandler>().isNotePanelOpen;

    }
}

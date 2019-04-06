using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteItem : Item
{
    //private NoteHandler noteHandler;

    private void Start()
    {
        //noteHandler = GameManagerSingleton.instance.GetComponent<NoteHandler>();
        player = GameManagerSingleton.instance.player.transform;
    }

    public override void Use()
    {
        //Debug.Log(noteHandler == null);
        if (GameManagerSingleton.instance.GetComponent<NoteHandler>().isNotePanelOpen == false)
        {
            GameManagerSingleton.instance.GetComponent<NoteHandler>().ReadCurrentFile();
        }

        SoundManager.instance.PlayRandomOneShot(SoundManager.instance.pageSounds);
        GameManagerSingleton.instance.GetComponent<NoteHandler>().isNotePanelOpen = !GameManagerSingleton.instance.GetComponent<NoteHandler>().isNotePanelOpen;

    }
}

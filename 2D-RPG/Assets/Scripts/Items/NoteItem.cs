using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteItem : Item
{
    private NoteHandler noteHandler;

    private void Start()
    {
        noteHandler = GameManagerSingleton.instance.GetComponent<NoteHandler>();
        player = GameManagerSingleton.instance.player.transform;
    }

    public override void Use()
    {
        if (!noteHandler.isNotePanelOpen)
        {
            noteHandler.ReadCurrentFile();
        }
        SoundManager.instance.PlayRandomOneShot(SoundManager.instance.pageSounds);

        noteHandler.isNotePanelOpen = !noteHandler.isNotePanelOpen;

    }
}

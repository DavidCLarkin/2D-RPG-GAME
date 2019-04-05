using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class NoteHandler : MonoBehaviour
{
    //string startPath = "Assets/TextFiles/";
    public TextAsset[] filesToRead;
    public RectTransform notePanel;
    [HideInInspector]
    public bool isNotePanelOpen;

    [HideInInspector]
    private int currentFile = 0;

    private void Update()
    {
        notePanel.gameObject.SetActive(isNotePanelOpen);
    }

    public void ReadPreviousFile()
    {
        if(currentFile > 0)
            currentFile--;
        if (currentFile <= 0)
            currentFile = 0;

        SetNoteText();
    }

    public void ReadCurrentFile()
    {
        SetNoteText();
    }

    public void ReadNextFile()
    {
        currentFile++;
        if (currentFile >= filesToRead.Length - 1)
            currentFile = filesToRead.Length - 1;

        SetNoteText();
    }

    public void SetNoteText()
    {
        Text[] texts = notePanel.GetComponentsInChildren<Text>();
        texts[0].text = filesToRead[currentFile].text;
        texts[1].text = (currentFile + 1).ToString();
    }
}

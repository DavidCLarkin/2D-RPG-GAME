using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    public string StatName { get; set; }
    public int StatLevel { get; set; }

    public Stat(int sLevel, string sName)
    {
        StatLevel = sLevel;
        StatName = sName;
    }
}

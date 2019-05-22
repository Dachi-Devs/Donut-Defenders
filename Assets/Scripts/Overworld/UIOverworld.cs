using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverworld : MonoBehaviour
{
    public Text worldTitle;
    public Text levelName;

    public void SetTitle(string title)
    {
        worldTitle.text = title;
    }

    public void SetLevelName(string level)
    {
        levelName.text = level;
    }
}

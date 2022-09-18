using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningThemePlayer : MonoBehaviour
{
    public void PlayOpeningTheme()
    {
        FindObjectOfType<AudioManager>().PlaySound("Opening Cutscene Music");
    }
    public void StopDialogueMusic()
    {
        FindObjectOfType<AudioManager>().StopSound("Dialogue Music");

    }
}

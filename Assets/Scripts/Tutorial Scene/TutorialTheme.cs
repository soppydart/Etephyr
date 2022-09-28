using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTheme : MonoBehaviour
{
    public void PlayTutTheme()
    {
        FindObjectOfType<AudioManager>().StopSound("Opening Cutscene Music");
        FindObjectOfType<AudioManager>().PlaySound("Tutorial Music");
    }
}

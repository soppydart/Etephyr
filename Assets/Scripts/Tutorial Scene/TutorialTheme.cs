using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTheme : MonoBehaviour
{
    public void PlayTutTheme()
    {
        FindObjectOfType<AudioManager>().PlaySound("Tutorial Music");
    }
}

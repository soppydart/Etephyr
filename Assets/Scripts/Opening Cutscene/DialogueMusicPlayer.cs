using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueMusicPlayer : MonoBehaviour
{
    void Start()
    {
        // FindObjectOfType<AudioManager>().PlaySound("Opening Cutscene Music");
        FindObjectOfType<AudioManager>().PlaySound("Dialogue Music");
    }
}

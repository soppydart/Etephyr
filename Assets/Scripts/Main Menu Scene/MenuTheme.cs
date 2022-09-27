using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTheme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlaySound("Main Menu Theme");
        Debug.Log("Theme started");
    }

    public void StopMainTheme()
    {
        FindObjectOfType<AudioManager>().StopSound("Main Menu Theme");
        // AudioManager.FadeOutSound("Main Menu Theme");
    }
}

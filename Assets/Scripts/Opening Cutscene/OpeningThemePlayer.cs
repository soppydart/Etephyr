using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningThemePlayer : MonoBehaviour
{
    public void PlayOpeningTheme()
    {
    }
    public void StopDialogueMusic()
    {
        FindObjectOfType<AudioManager>().StopSound("Dialogue Music");

    }
    private void Start()
    {
        StartCoroutine(PlaySound());

    }
    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(0f);
        FindObjectOfType<AudioManager>().PlaySound("Opening Cutscene Music");

    }
}

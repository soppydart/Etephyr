using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipBossCutscene : MonoBehaviour
{
    [SerializeField] Animator cameraAnimator;
    [SerializeField] GameObject[] DialogueCanvases;
    public bool cutscene1Skipped = false;
    void Start()
    {

    }

    public void Skip()
    {
        Debug.Log(cutscene1Skipped);
        if (!cutscene1Skipped)
        {
            FindObjectOfType<BossCutsceneController>().GetComponent<BossCutsceneController>().EndDialogue1();
        }
        else
        {
            FindObjectOfType<Boss>().GetComponent<Boss>().SkipDialogue2();
        }
        foreach (GameObject canvas in DialogueCanvases)
        {
            canvas.SetActive(false);
        }
    }
}

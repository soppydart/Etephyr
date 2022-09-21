using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCutscene : MonoBehaviour
{
    [SerializeField] Animator cameraAnimator;
    [SerializeField] GameObject[] DialogueCanvases;
    [SerializeField] GameObject Trigger;
    [SerializeField] GameObject Portal;
    [SerializeField] Animator witchAnimator;
    public bool cutscene1Skipped = false;
    void Start()
    {

    }

    public void Skip()
    {
        Debug.Log(cutscene1Skipped);
        if (!cutscene1Skipped)
        {
            cameraAnimator.SetTrigger("SkipCutscene");
            cutscene1Skipped = true;
            FindObjectOfType<CutsceneControllerMiniBoss>().GetComponent<CutsceneControllerMiniBoss>().endCutscene();
        }
        else
        {
            cameraAnimator.SetTrigger("SkipCutscene2");
            witchAnimator.SetTrigger("EndCutscene");
            FindObjectOfType<AudioManager>().StopSound("Dialogue Music");
            Portal.gameObject.SetActive(true);
            FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().StartMoving();
            FindObjectOfType<AudioManager>().GetComponent<TutorialTheme>().PlayTutTheme();
            Destroy(Trigger);
        }
        foreach (GameObject canvas in DialogueCanvases)
        {
            canvas.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] GameObject DialogueTrigger;
    [SerializeField] GameObject WitchDialogue0;
    [SerializeField] GameObject WitchName;
    [SerializeField] GameObject WitchAvatar;
    [SerializeField] Animator cameraAnimator;
    [SerializeField] GameObject portal;
    [SerializeField] Animator witchAnimator;
    bool dialoguesAllowed = false;
    PlayerMovement playerMovement;
    SidewaysMovingInstructions instructions;
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        instructions = FindObjectOfType<SidewaysMovingInstructions>();
    }
    void Update()
    {
        if (DialogueTrigger.gameObject.activeSelf)
        {
            dialoguesAllowed = true;
        }
        StartDialogue();
    }
    void StartDialogue()
    {
        if (dialoguesAllowed)
        {
            WitchDialogue0.gameObject.SetActive(true);
            WitchName.gameObject.SetActive(true);
            WitchAvatar.gameObject.SetActive(true);
        }
    }
    public void OpenPortal()
    {
        cameraAnimator.SetBool("cutscene1", true);
        StartCoroutine(PortalAnimationStart());
        StartCoroutine(StopCutScene());
    }
    IEnumerator StopCutScene()
    {
        yield return new WaitForSeconds(3);
        cameraAnimator.SetBool("cutscene1", false);
        StartCoroutine(WitchDisappears());
    }
    IEnumerator PortalAnimationStart()
    {
        yield return new WaitForSeconds(0.5f);
        portal.gameObject.SetActive(true);
    }
    IEnumerator WitchDisappears()
    {
        yield return new WaitForSeconds(1.8f);
        witchAnimator.SetBool("witchDisappears", true);
        StartCoroutine(DelayDisappear());
    }
    IEnumerator DelayDisappear()
    {
        yield return new WaitForSeconds(0.5f);
        witchAnimator.SetBool("witchDisappears", false);
        StartCoroutine(AllowPlayerToMove());
    }
    IEnumerator AllowPlayerToMove()
    {
        yield return new WaitForSeconds(2f);
        cameraAnimator.SetBool("isMovementAllowed", true);
        playerMovement.StartMoving();
        StartCoroutine(ShowSidewaysMovingInstructions());
    }
    [SerializeField] GameObject InstructionsCanvas;
    IEnumerator ShowSidewaysMovingInstructions()
    {
        yield return new WaitForSeconds(0.5f);
        InstructionsCanvas.SetActive(true);
        Time.timeScale = 0f;
        FindObjectOfType<PlayerCombat>().GetComponent<PlayerMovement>().StopMoving();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostMiniBossCutsceneController : MonoBehaviour
{
    [SerializeField] GameObject WitchDialogueCanvas;
    [SerializeField] GameObject Trigger;
    [SerializeField] GameObject Portal;
    [SerializeField] Animator witchAnimator;
    [SerializeField] Animator cameraAnimator;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Dialogue()
    {
        StartCoroutine(StartDialogue());
    }
    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(2f);
        WitchDialogueCanvas.gameObject.SetActive(true);
    }
    // public void EndCutscene()
    // {
    //     witchAnimator.SetTrigger("EndCutscene");
    //     Destroy(Trigger);
    // }
    public void ChangeCamera()
    {
        Debug.Log("ok");
        cameraAnimator.SetTrigger("CutSceneEnded");
    }
    public void EndDialogue()
    {
        cameraAnimator.SetTrigger("DialogueEnded");
        StartCoroutine(ActivatePortal());
    }
    IEnumerator ActivatePortal()
    {
        yield return new WaitForSeconds(2f);
        Portal.gameObject.SetActive(true);
        StartCoroutine(EndCutscene());
    }
    IEnumerator EndCutscene()
    {
        Debug.Log("HIIIII PPPP");
        yield return new WaitForSeconds(1f);
        StartCoroutine(ActivatePortal());
        cameraAnimator.ResetTrigger("DialogueEnded");
        StartCoroutine(DestroyWitch());
    }
    IEnumerator DestroyWitch()
    {
        yield return new WaitForSeconds(3f);
        witchAnimator.SetTrigger("EndCutscene");
        Destroy(Trigger);
        yield return new WaitForSeconds(0.2f);
        cameraAnimator.SetTrigger("CutSceneEnded");
        FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().StartMoving();
    }
}
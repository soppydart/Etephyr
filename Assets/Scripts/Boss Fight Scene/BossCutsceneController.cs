using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCutsceneController : MonoBehaviour
{
    [SerializeField] GameObject BossDialogueCanvas1;
    [SerializeField] Animator cameraAnimator;
    [SerializeField] Animator bossAnimator;
    [SerializeField] GameObject invisibleWall;
    [SerializeField] GameObject LetterPrompt;
    [SerializeField] GameObject BossHealthBar;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartDialogue1()
    {
        StartCoroutine(BossDialogue1());
    }
    IEnumerator BossDialogue1()
    {
        yield return new WaitForSeconds(1f);
        BossDialogueCanvas1.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().StopSound("Tutorial Music");
        FindObjectOfType<AudioManager>().PlaySound("Dialogue Music");
    }
    public void EndDialogue1()
    {
        cameraAnimator.SetTrigger("StartFight");
        invisibleWall.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().StopSound("Dialogue Music");
        FindObjectOfType<AudioManager>().PlaySound("Boss Music Intro");
        StartCoroutine(PlayLoopMusic1());
        StartCoroutine(StartBossFight());
        FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().StartMoving();
    }
    IEnumerator StartBossFight()
    {
        BossHealthBar.SetActive(true);
        yield return new WaitForSeconds(2f);
        bossAnimator.SetTrigger("StartPhase1");
    }
    IEnumerator PlayLoopMusic1()
    {
        yield return new WaitForSeconds(9f);
        FindObjectOfType<AudioManager>().StopSound("Boss Music Intro");
        FindObjectOfType<AudioManager>().PlaySound("Boss Music Loop");
    }
    public void Letter()
    {
        StartCoroutine(DisplayLetter());
    }
    IEnumerator DisplayLetter()
    {
        yield return new WaitForSeconds(3f);
        LetterPrompt.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneControllerMiniBoss : MonoBehaviour
{
    [SerializeField] Animator myAnimator;
    [SerializeField] Animator miniBossAnimator;
    [SerializeField] GameObject DialogueCanvas;
    [SerializeField] GameObject skipCanvas;
    public bool isInCutscene = false;
    bool cutsceneHasStarted = false;
    MiniBoss miniBoss;
    PlayerMovement player;
    void Start()
    {
        miniBoss = FindObjectOfType<MiniBoss>();
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInCutscene && !cutsceneHasStarted)
            StartCoroutine(BossMonologue());
    }
    IEnumerator BossMonologue()
    {
        cutsceneHasStarted = true;
        yield return new WaitForSeconds(1f);
        myAnimator.SetBool("isMiniBossSpeaking", true);
        yield return new WaitForSeconds(1.5f);
        DialogueCanvas.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().StopSound("Tutorial Music");
        FindObjectOfType<AudioManager>().PlaySound("Dialogue Music");
    }
    [SerializeField] GameObject MiniBossHealthBar;
    public void endCutscene()
    {
        myAnimator.SetBool("isMiniBossSpeaking", false);
        MiniBossHealthBar.SetActive(true);
        StartCoroutine("StartFight");
        FindObjectOfType<SkipCutscene>().GetComponent<SkipCutscene>().cutscene1Skipped = true;
        skipCanvas.SetActive(false);
    }
    IEnumerator StartFight()
    {
        yield return new WaitForSeconds(1f);
        miniBossAnimator.SetTrigger("startFight");
        miniBoss.isMovementAllowed = true;
        player.StartMoving();
        FindObjectOfType<AudioManager>().StopSound("Dialogue Music");
        FindObjectOfType<AudioManager>().PlaySound("Mini-Boss Music Intro");
        StartCoroutine(PlayLoopMusic());
    }
    IEnumerator PlayLoopMusic()
    {
        yield return new WaitForSeconds(3f);
        FindObjectOfType<AudioManager>().StopSound("Mini-Boss Music Intro");
        FindObjectOfType<AudioManager>().PlaySound("Mini-Boss Music Loop");
    }
}

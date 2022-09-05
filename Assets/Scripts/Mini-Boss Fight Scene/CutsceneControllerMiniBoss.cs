using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneControllerMiniBoss : MonoBehaviour
{
    [SerializeField] Animator myAnimator;
    [SerializeField] Animator miniBossAnimator;
    [SerializeField] GameObject DialogueCanvas;
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
    }
    [SerializeField] GameObject MiniBossHealthBar;
    public void endCutscene()
    {
        myAnimator.SetBool("isMiniBossSpeaking", false);
        MiniBossHealthBar.SetActive(true);
        StartCoroutine("StartFight");
    }
    IEnumerator StartFight()
    {
        yield return new WaitForSeconds(1f);
        miniBossAnimator.SetTrigger("startFight");
        miniBoss.isMovementAllowed = true;
        player.StartMoving();
    }
}

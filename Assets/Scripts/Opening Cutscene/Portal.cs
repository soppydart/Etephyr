using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    Animator portalAnimator;
    PlayerMovement playerMovement;
    GameController gameController;
    void Start()
    {
        portalAnimator = GetComponent<Animator>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        gameController = FindObjectOfType<GameController>();
    }
    bool playerHasEnteredPortal = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !playerHasEnteredPortal)
        {
            playerHasEnteredPortal = true;
            playerMovement.StopMoving();
            playerAnimator.SetBool("hasEnteredPortal", true);
            StartCoroutine(ClosePortal());
        }
    }
    IEnumerator ClosePortal()
    {
        yield return new WaitForSeconds(1f);
        portalAnimator.SetBool("isClosed", true);
        FindObjectOfType<AudioManager>().StopSound("Opening Cutscene Music");
        gameController.LoadNextScene();
    }
}

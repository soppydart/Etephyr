using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftBackCamera2 : MonoBehaviour
{
    [SerializeField] Animator tutorialCameraAnimator;
    // bool flag = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            tutorialCameraAnimator.SetTrigger("Back2");
            // if (flag)
            // {
            // StartCoroutine(ShowWallJumpInstructions());
            // flag = false;
            // }
        }
    }
    IEnumerator ShowWallJumpInstructions()
    {
        yield return new WaitForSeconds(0.5f);
        // WallJumpInstructions.SetActive(true);
        Time.timeScale = 0f;
        FindObjectOfType<PlayerCombat>().GetComponent<PlayerMovement>().StopMoving();
    }
}

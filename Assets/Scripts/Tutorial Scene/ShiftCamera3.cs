using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftCamera3 : MonoBehaviour
{
    [SerializeField] Animator tutorialCameraAnimator;
    [SerializeField] GameObject CombatInstructions;
    bool flag = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            tutorialCameraAnimator.SetBool("WallClimbTutorial", false);
            tutorialCameraAnimator.SetBool("CombatTutorial", true);
            if (flag)
            {
                StartCoroutine(ShowCombatInstructions());
                flag = false;
            }
        }
    }
    IEnumerator ShowCombatInstructions()
    {
        yield return new WaitForSeconds(0.5f);
        CombatInstructions.SetActive(true);
        Time.timeScale = 0f;
        FindObjectOfType<PlayerCombat>().GetComponent<PlayerMovement>().StopMoving();
    }
}

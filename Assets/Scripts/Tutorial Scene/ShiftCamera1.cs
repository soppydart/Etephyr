using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftCamera1 : MonoBehaviour
{
    [SerializeField] Animator tutorialCameraAnimator;
    [SerializeField] GameObject DashingInstructions;
    bool flag = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            tutorialCameraAnimator.SetBool("DashingTutorial", true);
            if (flag)
            {
                StartCoroutine(ShowDashInstructions());
                flag = false;
            }
        }
    }
    IEnumerator ShowDashInstructions()
    {
        yield return new WaitForSeconds(0.5f);
        DashingInstructions.SetActive(true);
        Time.timeScale = 0f;
        FindObjectOfType<PlayerCombat>().GetComponent<PlayerMovement>().StopMoving();
    }
}

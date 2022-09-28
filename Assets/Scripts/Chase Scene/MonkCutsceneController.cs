using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkCutsceneController : MonoBehaviour
{
    [SerializeField] Animator cameraAnimator;
    [SerializeField] Animator monkAnimator;
    bool flag = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" & !flag)
        {
            flag = true;
            other.GetComponent<PlayerMovement>().StopMoving();
            cameraAnimator.SetTrigger("Monk");
            StartCoroutine(ShiftCameraToWorld(other));
        }
    }
    IEnumerator ShiftCameraToWorld(Collider2D player)
    {
        yield return new WaitForSeconds(2f);
        player.GetComponent<PlayerMovement>().StartMoving();
        FindObjectOfType<AudioManager>().StopSound("Tutorial Music");
        FindObjectOfType<AudioManager>().PlaySound("Chase Music");
        cameraAnimator.SetTrigger("Chase");
        monkAnimator.SetTrigger("StartChase");
    }
}

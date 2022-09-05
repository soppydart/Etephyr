using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{
    [SerializeField] Animator cameraAnimator;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            cameraAnimator.SetTrigger("ShowArena");
            other.GetComponent<PlayerMovement>().StopMoving();
            Destroy(gameObject, 2f);
        }
    }
}

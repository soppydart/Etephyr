using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostMiniBossCutsceneTrigger : MonoBehaviour
{
    [SerializeField] Animator myAnimator;
    [SerializeField] GameObject invisibleWall;
    [SerializeField] GameObject Witch;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().StopMoving();
            myAnimator.SetTrigger("Post Fight Cutscene");
            invisibleWall.gameObject.SetActive(true);
            Witch.gameObject.SetActive(true);
        }
    }
}

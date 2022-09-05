using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            FindObjectOfType<GameController>().GetComponent<GameController>().LoadNextSceneInstantly();
    }
}

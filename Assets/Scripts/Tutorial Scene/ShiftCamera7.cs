using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShiftCamera7 : MonoBehaviour
{
    [SerializeField] GameObject FadeOutCanvas;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FadeOutCanvas.SetActive(true);
            // other.GetComponent<PlayerMovement>().StopMoving();
            FindObjectOfType<GameController>().LoadNextScene();
        }
    }
}

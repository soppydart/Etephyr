using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] GameObject GameOverCanvas;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<StopWatch>().GetComponent<StopWatch>().StopTime();
            other.GetComponent<PlayerMovement>().StopMoving();
            GameOverCanvas.SetActive(true);
        }
    }
}

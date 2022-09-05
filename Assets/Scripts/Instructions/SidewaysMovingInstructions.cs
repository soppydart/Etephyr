using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysMovingInstructions : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] GameObject SidewaysMovingInstructionsCanvas;
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        CloseSidewaysMovingInstructionsCanvas();
    }
    public void ShowSidewaysMovingInstructions()
    {
        SidewaysMovingInstructionsCanvas.gameObject.SetActive(true);
    }
    void CloseSidewaysMovingInstructionsCanvas()
    {
        if (playerMovement.playerHasMovedOnce)
            StartCoroutine(CloseSidewaysMovingInstructions());
    }
    IEnumerator CloseSidewaysMovingInstructions()
    {
        yield return new WaitForSeconds(1f);
        SidewaysMovingInstructionsCanvas.gameObject.SetActive(false);
    }
}

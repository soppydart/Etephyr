using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingInstructions : MonoBehaviour
{
    [SerializeField] GameObject JumpingInstruction;
    private void Start()
    {
        JumpingInstruction.SetActive(true);
        // Time.timeScale = 0f;
        FindObjectOfType<PlayerCombat>().GetComponent<PlayerMovement>().StopMoving();
    }
}

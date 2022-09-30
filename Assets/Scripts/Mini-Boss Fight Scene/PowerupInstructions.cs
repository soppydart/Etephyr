using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInstructions : MonoBehaviour
{
    [SerializeField] GameObject Instructions;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instructions.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftCamera5 : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.SetTrigger("Falling Platforms Tutorial");
        }
    }
}

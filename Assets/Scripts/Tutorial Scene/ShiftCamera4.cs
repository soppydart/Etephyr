using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftCamera4 : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.SetTrigger("Moving Platforms Tutorial 1");
        }
    }
}

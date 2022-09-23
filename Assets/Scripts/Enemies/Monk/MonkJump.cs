using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkJump : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            // FindObjectOfType<Monk>().isJumping = true;
        }
        else { }
        // FindObjectOfType<Monk>().isJumping = false;
    }
}

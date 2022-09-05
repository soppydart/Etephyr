using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformsTrigger : MonoBehaviour
{
    [SerializeField] Rigidbody2D fallingPlatform;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            fallingPlatform.gravityScale = 3f;
        }
    }
}

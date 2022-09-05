using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryVerticallyMovingPlatform : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Vertically Moving Platform")
        {
            other.GetComponent<VerticallyMovingPlatform>().direction *= -1;
            Debug.Log("Hello");
        }
    }
}

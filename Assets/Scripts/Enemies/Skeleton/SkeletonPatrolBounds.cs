using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPatrolBounds : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Skeleton>().FlipSkeleton();
        }
    }
}

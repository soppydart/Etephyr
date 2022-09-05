using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticallyMovingPlatform : MonoBehaviour
{
    public int direction = 1;
    [SerializeField] float moveSpeed = 2f;
    void Update()
    {
        if (direction == 1)
            transform.position += Vector3.up * Time.deltaTime * moveSpeed;
        else
            transform.position += Vector3.down * Time.deltaTime * moveSpeed;
    }
}

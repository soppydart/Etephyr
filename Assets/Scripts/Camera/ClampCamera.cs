using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampCamera : MonoBehaviour
{
    [SerializeField] Transform leftBoundary;
    [SerializeField] Transform rightBoundary;
    [SerializeField] Transform topBoundary;
    [SerializeField] Transform bottomBoundary;
    [SerializeField] Transform player;
    [SerializeField] float raiseCameraMargin = 2f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3
        (Mathf.Clamp(player.position.x, leftBoundary.position.x, rightBoundary.position.x),
        Mathf.Clamp(player.position.y, bottomBoundary.position.y, topBoundary.position.y) + raiseCameraMargin,
        transform.position.z);
    }
}

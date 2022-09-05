using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    float length, startPos;
    [SerializeField] GameObject movingCamera;
    [SerializeField] float parallaxEffect;
    Vector3 previousCameraPosition;
    public bool parallaxAllowed = true;
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        previousCameraPosition = movingCamera.transform.position;
    }
    void Update()
    {
        if (!parallaxAllowed)
            return;
        float relativeDistanceMoved = movingCamera.transform.position.x * (1 - parallaxEffect);
        float distance = movingCamera.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        if (relativeDistanceMoved > startPos + length)
            startPos += length;
        else if (relativeDistanceMoved < startPos - length)
            startPos -= length;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float duration = 0.1f;
    [SerializeField] float magnitude = 5f;
    public void Shake()
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            Vector3 newPosition = Random.insideUnitSphere * (Time.deltaTime * magnitude);
            transform.localPosition = new Vector3(newPosition.x, newPosition.y, originalPosition.z);
            elapsed += Time.deltaTime;
        }
        transform.localPosition = originalPosition;
    }
}

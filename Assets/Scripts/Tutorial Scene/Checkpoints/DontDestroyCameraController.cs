using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCameraController : MonoBehaviour
{
    private void Awake()
    {
        int n = FindObjectsOfType<DontDestroyCameraController>().Length;
        if (n > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
}

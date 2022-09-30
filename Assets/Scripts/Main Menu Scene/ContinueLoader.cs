using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueLoader : MonoBehaviour
{
    [SerializeField] Button Continue;
    private void Start()
    {
        Debug.Log("saved scene is " + SaveManager.Load().sceneNumber);
        int n = SaveManager.Load().sceneNumber;
        if (n == 0)
            Continue.interactable = false;
    }
}

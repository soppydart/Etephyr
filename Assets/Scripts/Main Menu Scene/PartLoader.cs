using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartLoader : MonoBehaviour
{
    [SerializeField] Button[] loadButtons;
    private void Start()
    {
        int n = SaveManager.Load().sceneNumber;
        for (int i = 0; i < n; i++)
        {
            loadButtons[i].interactable = true;
        }
    }
}

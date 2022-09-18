using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().StopSound("Ending Music");
        SceneManager.LoadScene(0);
    }
}

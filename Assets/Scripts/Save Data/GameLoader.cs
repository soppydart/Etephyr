using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    [SerializeField] Animator cameraAnimator;
    string[] tutorialTriggers = {"DashingTutorial", "WallClimbTutorial", "CombatTutorial", "Moving Platforms Tutorial 1"
    ,"Falling Platforms Tutorial","Breaking Platforms Tutorial"};
    private void Awake()
    {
        int n = FindObjectsOfType<GameLoader>().Length;
        if (n > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        // cameraAnimator = GameObject.Find("Camera Controller").GetComponent<Animator>();
    }
    public void LoadGame(GameData gameData)
    {
        // SceneManager.LoadScene(gameData.sceneNumber);
        cameraAnimator = GameObject.Find("Camera Controller").GetComponent<Animator>();
        // if (FindObjectOfType<PlayerCombat>().GetComponent<PlayerCombat>().GetHealth() <= 0)
        for (int i = 0; i < gameData.checkpointNumber; i++)
        {
            cameraAnimator.SetTrigger(tutorialTriggers[i]);
            Debug.Log("Camera shifted");
        }
        FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().SetPosition(gameData.playerPosition);
        FindObjectOfType<AudioManager>().GetComponent<AudioManager>().IncreasePitch();
        FindObjectOfType<PlayerCombat>().GetComponent<PlayerCombat>().SetHealth(gameData.health);
        FindObjectOfType<PlayerCombat>().GetComponent<PlayerCombat>().showGameOver = true;
        Debug.Log("Game Loaded");
        // FindObjectOfType<PlayerMovement>().GetComponent<Animator>().SetTrigger("Revive");
    }
    public void LoadFromContinue(GameData gameData)
    {
        SceneManager.LoadScene(gameData.sceneNumber);
    }
}

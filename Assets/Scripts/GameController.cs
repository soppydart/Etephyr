using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // PlayerMovement playerMovement;
    // [SerializeField] GameObject SidewaysMovingInstructionsCanvas;
    // [SerializeField] GameObject JumpingInstructionsCanvas;
    // void Start()
    // {
    //     playerMovement = FindObjectOfType<PlayerMovement>();
    // }
    // void Update()
    // {
    //     CloseSidewaysMovingInstructionsCanvas();
    //     CloseJumpingInstructionsCanvas();
    // }
    public void LoadNextScene()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void StartThisGame()
    {
        MainMenuAnimator.SetTrigger("StartGame");
        StartCoroutine(StartThis());
    }
    [SerializeField] Animator MainMenuAnimator;
    IEnumerator StartThis()
    {
        yield return new WaitForSeconds(1f);
        LoadNextScene();
    }
    public void LoadNextSceneInstantly()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    [SerializeField] GameObject InstructionsCanvas;
    [SerializeField] GameObject InstructionsCanvas1;
    [SerializeField] GameObject InstructionsCanvas2;
    [SerializeField] GameObject InstructionsCanvas3;
    public void CloseInstructions()
    {
        Time.timeScale = 1f;
        InstructionsCanvas.SetActive(false);
        FindObjectOfType<PlayerCombat>().GetComponent<PlayerMovement>().StartMoving();
    }
    public void CloseInstructions1()
    {
        Time.timeScale = 1f;
        InstructionsCanvas1.SetActive(false);
        FindObjectOfType<PlayerCombat>().GetComponent<PlayerMovement>().StartMoving();
    }
    public void CloseInstructions2()
    {
        Time.timeScale = 1f;
        InstructionsCanvas2.SetActive(false);
        FindObjectOfType<PlayerCombat>().GetComponent<PlayerMovement>().StartMoving();
    }
    public void CloseInstructions3()
    {
        Time.timeScale = 1f;
        InstructionsCanvas3.SetActive(false);
        FindObjectOfType<PlayerCombat>().GetComponent<PlayerMovement>().StartMoving();
    }
    // public void SidewaysMovingInstructions()
    // {
    //     SidewaysMovingInstructionsCanvas.gameObject.SetActive(true);
    // }
    // bool SidewaysMovingInstructionsCanvasClosed = false;
    // void CloseSidewaysMovingInstructionsCanvas()
    // {
    //     if (playerMovement.playerHasMovedOnce && !SidewaysMovingInstructionsCanvasClosed)
    //     {
    //         StartCoroutine(CloseSidewaysMovingInstructions());
    //         SidewaysMovingInstructionsCanvasClosed = true;
    //     }
    // }
    // IEnumerator CloseSidewaysMovingInstructions()
    // {
    //     yield return new WaitForSeconds(1f);
    //     SidewaysMovingInstructionsCanvas.gameObject.SetActive(false);
    // }
    // bool JumpingInstructionsCanvasClosed = false;
    // void CloseJumpingInstructionsCanvas()
    // {
    //     if (playerMovement.hasJumpedOnce && !JumpingInstructionsCanvasClosed)
    //         StartCoroutine(CloseJumpingInstructions());
    // }
    // IEnumerator CloseJumpingInstructions()
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     JumpingInstructionsCanvas.gameObject.SetActive(false);
    // }
}

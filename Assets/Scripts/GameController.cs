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
        FindObjectOfType<AudioManager>().IncreasePitch();
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
        FindObjectOfType<AudioManager>().IncreasePitch();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReloadScene()
    {
        FindObjectOfType<AudioManager>().IncreasePitch();
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
    [SerializeField] GameObject MainMenuLoader;
    bool flag = false;
    public void SkipCutscene()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Mini-boss Fight Scene"))
        {
            if (!flag)
            {
                FindObjectOfType<CutsceneControllerMiniBoss>().GetComponent<CutsceneControllerMiniBoss>().skipped = true;
                FindObjectOfType<SkipCutscene>().GetComponent<SkipCutscene>().Skip();
                flag = true;
            }
            else
            {
                FindObjectOfType<PostMiniBossCutsceneController>().GetComponent<PostMiniBossCutsceneController>().skipped = true;
                FindObjectOfType<SkipCutscene>().GetComponent<SkipCutscene>().Skip();
            }
        }

        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Boss Fight Scene"))
        {
            FindObjectOfType<BossCutsceneController>().GetComponent<BossCutsceneController>().skipped = true;
            FindObjectOfType<SkipBossCutscene>().GetComponent<SkipBossCutscene>().Skip();
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Opening Cutscene"))
        {
            LoadNextSceneInstantly();
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Ending Scene"))
        {
            // FindObjectOfType<SkipEndingCutscene>().GetComponent<SkipEndingCutscene>().Skip();
            FindObjectOfType<AudioManager>().StopSound("Ending Music");
            Destroy(FindObjectOfType<AudioManager>());
            MainMenuLoader.SetActive(true);
        }
    }
    public void LoadArcadeMode()
    {
        SceneManager.LoadScene("Procedurally Generated Scene");
        Destroy(FindObjectOfType<AudioManager>());
    }
    public void LoadGameFromFile()
    {
        FindObjectOfType<GameLoader>().LoadGame(SaveManager.Load());
    }
    public void LoadGameFromFileContinue()
    {
        FindObjectOfType<GameLoader>().LoadFromContinue(SaveManager.Load());
    }
    public int GetSceneNumber()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    //Part Loader functions
    public void Part1()
    {
        SceneManager.LoadScene(1);
    }
    public void Part2()
    {
        SceneManager.LoadScene(2);
    }
    public void Part3()
    {
        SceneManager.LoadScene(3);
    }
    public void Part4()
    {
        SceneManager.LoadScene(4);
    }
    public void Part5()
    {
        SceneManager.LoadScene(5);
    }
}

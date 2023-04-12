using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// UIController - Controls UI related functionality in a level.
public class UIController : MonoBehaviour
{

    int MAIN_MENU_BUILD_INDEX = 0;
    [SerializeField] GameObject PauseUI;
    [SerializeField] GameObject CompleteUI;
    [SerializeField] GameObject FailedUI;
    [SerializeField] LevelTracker levelTracker;
    public bool isUIVisible;

    private void Start() {
        isUIVisible = false;
    }

    // Functionality to Display Level Complete UI.
    public void DisplayGameComplete() {
        isUIVisible = true;
        AudioManager.Instance.PlayAudio(AudioType.LEVEL_COMPLETE);
        PauseUI.SetActive(false);
        FailedUI.SetActive(false);
        CompleteUI.SetActive(true);
    }

    // Functionality to Display Level Paused UI.
    public void DisplayGamePaused() {
        isUIVisible = true;
        AudioManager.Instance.PlayAudio(AudioType.LEVEL_PAUSED);
        FailedUI.SetActive(false);
        CompleteUI.SetActive(false);
        PauseUI.SetActive(true);
    }

    // Functionality to Display Level Failed UI.
    public void DisplayGameFailed() {
        isUIVisible = true;
        AudioManager.Instance.PlayAudio(AudioType.LEVEL_FAILED);
        PauseUI.SetActive(false);
        CompleteUI.SetActive(false);
        FailedUI.SetActive(true);
    }

    // Functionality on Clicking Restart Button.
    public void OnRestartButtonClick() {
        isUIVisible = false;
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex % SceneManager.sceneCountInBuildSettings);
    }

    // Functionality on Clicking Main Menu Button.
    public void MainMenuButtonClick() {
        isUIVisible = false;
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        SceneManager.LoadScene(MAIN_MENU_BUILD_INDEX);
    }

    // Functionality on Clicking Next Level Button.
    public void NextLevelButtonClick() {
        isUIVisible = false;
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    // Functionality on Clicking Resume Button.
    public void OnResumeButtonClick() {
        isUIVisible = false;
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        PauseUI.SetActive(false);
        levelTracker.isGamePaused = false;
    }
}

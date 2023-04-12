using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    public void DisplayGameComplete() {
        isUIVisible = true;
        AudioManager.Instance.PlayAudio(AudioType.LEVEL_COMPLETE);
        PauseUI.SetActive(false);
        FailedUI.SetActive(false);
        CompleteUI.SetActive(true);
    }

    public void DisplayGamePaused() {
        isUIVisible = true;
        AudioManager.Instance.PlayAudio(AudioType.LEVEL_PAUSED);
        FailedUI.SetActive(false);
        CompleteUI.SetActive(false);
        PauseUI.SetActive(true);
    }

    public void DisplayGameFailed() {
        isUIVisible = true;
        AudioManager.Instance.PlayAudio(AudioType.LEVEL_FAILED);
        PauseUI.SetActive(false);
        CompleteUI.SetActive(false);
        FailedUI.SetActive(true);
    }

    public void OnRestartButtonClick() {
        isUIVisible = false;
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex % SceneManager.sceneCountInBuildSettings);
    }

    public void MainMenuButtonClick() {
        isUIVisible = false;
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        SceneManager.LoadScene(MAIN_MENU_BUILD_INDEX);
    }

    public void NextLevelButtonClick() {
        isUIVisible = false;
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    public void OnResumeButtonClick() {
        isUIVisible = false;
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        PauseUI.SetActive(false);
        levelTracker.isGamePaused = false;
    }
}

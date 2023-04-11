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

    // Update is called once per frame
    void Update()
    {
        if (levelTracker.isGameComplete) {
            PauseUI.SetActive(false);
            FailedUI.SetActive(false);
            CompleteUI.SetActive(true);
        } else if (levelTracker.isGamePaused) {
            FailedUI.SetActive(false);
            CompleteUI.SetActive(false);
            PauseUI.SetActive(true);
        } else if (levelTracker.isGameOver) {
            PauseUI.SetActive(false);
            CompleteUI.SetActive(false);
            FailedUI.SetActive(true);
        } else {
            PauseUI.SetActive(false);
            CompleteUI.SetActive(false);
            FailedUI.SetActive(false);
        }
    }

    public void OnRestartButtonClick() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex % SceneManager.sceneCountInBuildSettings);
    }

    public void MainMenuButtonClick() {
        SceneManager.LoadScene(MAIN_MENU_BUILD_INDEX);
    }

    public void NextLevelButtonClick() {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    public void OnResumeButtonClick() {
        PauseUI.SetActive(false);
        levelTracker.isGamePaused = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// LobbyController Class - Keeps track of UI in Lobby Scene and manages Level Unlocking System.
public class LobbyController : MonoBehaviour
{
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameObject LevelSelectUI;
    [SerializeField] Button[] levelButtons;
    Image[] levelButtonImages;
    int levels;
    [SerializeField] GameObject HowToPlayUI;
    // Start is called before the first frame update
    void Start()
    {
        levels = SceneManager.sceneCountInBuildSettings - 1;
        levelButtonImages = new Image[levelButtons.Length];
        for (int i = 0; i < levelButtons.Length; i++) {
            levelButtons[i].enabled = false;
            levelButtonImages[i] = levelButtons[i].GetComponent<Image>();
            levelButtonImages[i].color = Constants.LEVEL_BUTTON_DISABLED_COLOR;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Mathf.Min(PlayerPrefs.GetInt("LEVEL", 1), levels); i++) {
            levelButtons[i].enabled = true;
            levelButtonImages[i].color = Constants.LEVEL_BUTTON_ENABLED_COLOR;
        }
            
    }

    // Functionality on Clicking Level Buttons.
    public void OnLevelButtonClick(string levelName) {
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        SceneManager.LoadScene(levelName);
    }

    // Functionality on cicking Back Button.
    public void OnBackButtonClick() {
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        HowToPlayUI.SetActive(false);
        LevelSelectUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    // Functionality on clicking How to Play Button
    public void OnHowToPlayButtonClick() {
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        LevelSelectUI.SetActive(false);
        MainMenuUI.SetActive(false);
        HowToPlayUI.SetActive(true);
    }

    // Functionality on Clicking Start Button.
    public void OnStartButtonClick() {
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        HowToPlayUI.SetActive(false);
        MainMenuUI.SetActive(false);
        LevelSelectUI.SetActive(true);
    }

    // Functionality on clicking Quit Button.
    public void OnQuitButtonClick() {
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        Application.Quit();
    }
}

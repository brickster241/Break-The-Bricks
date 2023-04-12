using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public void OnLevelButtonClick(string levelName) {
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        SceneManager.LoadScene(levelName);
    }

    public void OnBackButtonClick() {
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        HowToPlayUI.SetActive(false);
        LevelSelectUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void OnHowToPlayButtonClick() {
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        LevelSelectUI.SetActive(false);
        MainMenuUI.SetActive(false);
        HowToPlayUI.SetActive(true);
    }

    public void OnStartButtonClick() {
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        HowToPlayUI.SetActive(false);
        MainMenuUI.SetActive(false);
        LevelSelectUI.SetActive(true);
    }

    public void OnQuitButtonClick() {
        AudioManager.Instance.PlayAudio(AudioType.BUTTON_CLICK);
        Application.Quit();
    }
}

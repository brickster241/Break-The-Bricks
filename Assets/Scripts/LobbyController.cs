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
    [SerializeField] GameObject HowToPlayUI;
    // Start is called before the first frame update
    void Start()
    {
        levelButtonImages = new Image[levelButtons.Length];
        for (int i = 0; i < levelButtons.Length; i++) {
            levelButtons[i].enabled = false;
            levelButtonImages[i] = levelButtons[i].GetComponent<Image>();
            levelButtonImages[i].color = new Color(0.4f, 0.7f, 0.7f);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("LEVEL", 1); i++) {
            levelButtons[i].enabled = true;
            levelButtonImages[i].color = Color.yellow;
        }
            
    }

    public void OnLevelButtonClick(string levelName) {
        SceneManager.LoadScene(levelName);
    }

    public void OnBackButtonClick() {
        HowToPlayUI.SetActive(false);
        LevelSelectUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void OnHowToPlayButtonClick() {
        LevelSelectUI.SetActive(false);
        MainMenuUI.SetActive(false);
        HowToPlayUI.SetActive(true);
    }

    public void OnStartButtonClick() {
        HowToPlayUI.SetActive(false);
        MainMenuUI.SetActive(false);
        LevelSelectUI.SetActive(true);
    }

    public void OnQuitButtonClick() {
        Application.Quit();
    }
}

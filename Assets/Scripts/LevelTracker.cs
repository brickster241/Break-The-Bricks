using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTracker : MonoBehaviour
{

    
    [SerializeField] int numberOfBricks;
    [SerializeField] SquareBrickController[] bricks;
    [SerializeField] BallLauncher ballManager;
    public bool isDecreasedHeight = false;
    public bool isGameOver = false;
    public bool isGameComplete = false;
    public bool isGamePaused = false;

    void NextLevel() {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    private void Start() {
        numberOfBricks = bricks.Length;
        isGameOver = false;
        isGameComplete = false;
        isGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isGamePaused = true;
        if (isGamePaused)
            return;
        if (numberOfBricks == 0) {
            isGameComplete = true;
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            int currMax = PlayerPrefs.GetInt("LEVEL", 1);
            PlayerPrefs.SetInt("LEVEL", Mathf.Max(currentLevel + 1, currMax));
        }
        if (ballManager.allBallsFetched && !isDecreasedHeight) {
            isDecreasedHeight = true;
            foreach (SquareBrickController brick in bricks) {
                brick.decreaseHeight();
            }
        } 
        
    }

    public void DecreaseBrickCount() {
        numberOfBricks -= 1;
    }
}

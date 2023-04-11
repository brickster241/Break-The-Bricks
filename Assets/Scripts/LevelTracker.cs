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

    void NextLevel() {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    private void Start() {
        numberOfBricks = bricks.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfBricks == 0) {
            NextLevel();
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

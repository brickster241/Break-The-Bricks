using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTracker : MonoBehaviour
{

    
    [SerializeField] int numberOfBricks;
    [SerializeField] SquareBrickController[] bricks;
    [SerializeField] ParticleSystem explosion_ps_bomb;
    [SerializeField] ParticleSystem explosion_ps_green;
    [SerializeField] ParticleSystem explosion_ps_blue;
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

    public ParticleSystem GetExplosionPSPrefab(BrickType brickType) {
        if (brickType == BrickType.OSCILLATING) {
            return explosion_ps_blue;
        } else if (brickType == BrickType.NONOSCILLATING) {
            return explosion_ps_green;
        } else {
            return explosion_ps_bomb;
        }
    }

    public void DecreaseBrickCount() {
        numberOfBricks -= 1;
    }
}

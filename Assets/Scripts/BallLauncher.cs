using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Class which keeps track of all the Balls.
public class BallLauncher : MonoBehaviour
{
    [SerializeField] Transform ArrowHead;
    [SerializeField] Camera mainCamera;
    [SerializeField] int NumberOfBalls;
    [SerializeField] BallMovement[] balls;
    [SerializeField] GameObject BallPrefab;
    [SerializeField] Transform BallParent;
    [SerializeField] LevelTracker levelTracker;
    public bool allBallsFetched;

    private void Awake() {
        balls = new BallMovement[NumberOfBalls];
        for (int i = 0; i < NumberOfBalls; i++) {
            balls[i] = Instantiate(BallPrefab, BallParent).GetComponent<BallMovement>();
            balls[i].SetHolder(transform, levelTracker);
            balls[i].ResetBall();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        allBallsFetched = true;
    }

    private void Update() {
        if (levelTracker.isGamePaused)
            return;
        setBallsFetched();
        Mouse mouse = Mouse.current;
        Vector3 mousePosition = mouse.position.ReadValue();
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePosition);
        worldPos.z = 0f;
        Vector2 direction = new Vector2(worldPos.x - transform.position.x, worldPos.y - transform.position.y).normalized;
        Vector3 position = ArrowHead.localPosition;
        ArrowHead.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, direction));
        position.x = direction.x * 2;
        position.y = direction.y * 2;
        ArrowHead.localPosition = position;  
        if (mouse.leftButton.wasPressedThisFrame && allBallsFetched && ArrowHead.rotation.z > 0) {
            StartCoroutine(setDirectionOfBalls(mouse));
        }
    }

    // Sets Direction of Balls based on Mouse Position.
    IEnumerator setDirectionOfBalls(Mouse mouse) {
        allBallsFetched = false;
        foreach (BallMovement mvt in balls)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePosition);
            worldPos.z = 0f;
            Vector2 direction = new Vector2(worldPos.x - transform.position.x, worldPos.y - transform.position.y).normalized;    
            mvt.startMovement(direction);
            yield return new WaitForSeconds(Constants.SET_DIRECTION_BALLS_INTERVAL);
        }
        levelTracker.isDecreasedHeight = false;
    }

    // Checks if ALL Balls are back to Launcher.
    void setBallsFetched() {
        foreach (BallMovement mvt in balls)
        {
            if (!mvt.isTurnComplete) {
                allBallsFetched = false;
                return;
            }
        }
        allBallsFetched = true;
    }
}

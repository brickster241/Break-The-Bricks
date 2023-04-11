using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
            balls[i].SetHolder(transform);
            balls[i].ResetBall();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        allBallsFetched = true;
    }

    private void Update() {
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
            StartCoroutine(setDirectionOfBalls(direction));
        }
    }

    IEnumerator setDirectionOfBalls(Vector2 direction) {
        allBallsFetched = false;
        Debug.Log(direction);
        foreach (BallMovement mvt in balls)
        {
            mvt.startMovement(direction);
            yield return new WaitForSeconds(0.2f);
        }
        levelTracker.isDecreasedHeight = false;
    }


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

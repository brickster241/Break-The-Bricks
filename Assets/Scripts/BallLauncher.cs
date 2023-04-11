using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] int NumberOfBalls;
    [SerializeField] GameObject[] balls;
    [SerializeField] GameObject BallPrefab;
    [SerializeField] Transform BallParent;
    bool allBallsFetched;

    private void Awake() {
        balls = new GameObject[NumberOfBalls];
        for (int i = 0; i < NumberOfBalls; i++) {
            balls[i] = Instantiate(BallPrefab, BallParent);
            BallMovement mvt = balls[i].GetComponent<BallMovement>();
            mvt.SetHolder(transform);
            mvt.ResetBall();
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
        if (mouse.leftButton.wasPressedThisFrame && allBallsFetched) {
            Vector3 mousePosition = mouse.position.ReadValue();
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePosition);
            worldPos.z = 0f;
            StartCoroutine(setDirectionOfBalls(new Vector2(worldPos.x, worldPos.y)));
        }
    }

    IEnumerator setDirectionOfBalls(Vector2 mouse_pos) {
        allBallsFetched = false;
        Vector2 direction = (mouse_pos - new Vector2(transform.position.x, transform.position.y)).normalized;
        Debug.Log(direction);
        foreach (GameObject ballMovt in balls)
        {
            BallMovement mvt = ballMovt.GetComponent<BallMovement>();
            mvt.startMovement(direction);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void setBallsFetched() {
        foreach (GameObject ballMovt in balls)
        {
            BallMovement mvt = ballMovt.GetComponent<BallMovement>();
            if (!mvt.isTurnComplete) {
                allBallsFetched = false;
                return;
            }
        }
        allBallsFetched = true;
    }
}

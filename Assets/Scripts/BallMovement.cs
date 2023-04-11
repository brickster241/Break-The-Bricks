using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    Transform BALL_HOLDER;
    [SerializeField] float X_BOUND;
    [SerializeField] Vector2 direction;
    public bool isTurnComplete = true;
    [SerializeField] LevelTracker levelTracker;
    [SerializeField] float speed;
    

    public void SetHolder(Transform holder_transform, LevelTracker levelTracker) {
        BALL_HOLDER = holder_transform;
        this.levelTracker = levelTracker;
    }

    public void ResetBall() {
        isTurnComplete = true;
        transform.position = BALL_HOLDER.position;
    }

    public void startMovement(Vector2 direction) {
        this.direction = direction;
        isTurnComplete = false;
    }

    private void FixedUpdate() {
        if (levelTracker.isGamePaused)
            return;
        direction = direction.normalized;
        Vector3 pos = transform.position;
        pos.y += direction.y * speed * Time.fixedDeltaTime;
        if ((isTurnComplete && Mathf.Abs(pos.x + direction.x * speed * Time.fixedDeltaTime) < X_BOUND) || !isTurnComplete) {
            pos.x += direction.x * speed * Time.fixedDeltaTime;    
        }
        transform.position = pos;
        if (isTurnComplete)
            BALL_HOLDER.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurnComplete) {
            if (Input.GetKey(KeyCode.A)) {
                direction = Vector2.left;
            } else if (Input.GetKey(KeyCode.D)) {
                direction = Vector2.right;
            } else {
                direction = Vector2.zero;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Tag : " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Top-Wall")) {
            direction.y = -direction.y;
        } else if (other.gameObject.CompareTag("Vertical-Wall")) {
            direction.x = -direction.x;
        } else if (other.gameObject.CompareTag("Square-Brick")) {
            // HANDLE SQUARE BRICK
            HandleSquareBrickCollision(other.gameObject);
        } else if (other.gameObject.CompareTag("Ground")) {
            // HANDLE GROUND
            StartCoroutine(BackToHolder());
        }
    }

    void HandleSquareBrickCollision(GameObject brick) {
        // FIND A WAY TO DETECT SIDES AND MAKE CHANGES ACCORDINGLY.
        // USE CROSS PRODUCT - FIND RIGHT DIAGONAL & LEFT DIAGONAL VECTOR
        BoxCollider2D brick_bc2d = brick.GetComponent<BoxCollider2D>();
        Vector2 extents = brick_bc2d.bounds.extents;
        Vector2 center = brick_bc2d.bounds.center;
        
        Vector2 topRight = new Vector2(center.x + extents.x, center.y + extents.y);
        Vector2 topLeft = new Vector2(center.x - extents.x, center.y + extents.y);
        Vector2 bottomLeft = new Vector2(center.x - extents.x, center.y - extents.y);
        Vector2 bottomRight = new Vector2(center.x + extents.x, center.y - extents.y);
        bool isLeftOfRightDiagonal = IsLeft(topRight - bottomLeft, new Vector2(transform.position.x, transform.position.y) - bottomLeft);
        bool isLeftOfLeftDiagonal = IsLeft(topLeft - bottomRight, new Vector2(transform.position.x, transform.position.y) - bottomRight);
        Debug.Log("RIGHT DIAGONAL DIRECTION : " + (isLeftOfRightDiagonal ? "LEFT" : "RIGHT"));
        Debug.Log("LEFT DIAGONAL DIRECTION : " + (isLeftOfLeftDiagonal ? "LEFT" : "RIGHT"));
        if (isLeftOfLeftDiagonal && isLeftOfRightDiagonal) {
            direction.x = -Mathf.Abs(direction.x);
        } else if (isLeftOfLeftDiagonal && !isLeftOfRightDiagonal) {
            direction.y = -Mathf.Abs(direction.y);
        } else if (!isLeftOfLeftDiagonal && isLeftOfRightDiagonal) {
            direction.y = Mathf.Abs(direction.y);
        } else {
            direction.x = Mathf.Abs(direction.x);
        }
        direction = direction.normalized;
    }

    bool IsLeft(Vector2 AB, Vector2 CB)
    {
        return (-AB.x * CB.y + AB.y * CB.x < 0);
    }


    IEnumerator BackToHolder() {
        while (transform.position != BALL_HOLDER.position) {
            direction = Vector2.zero;
            transform.position = Vector3.MoveTowards(transform.position, BALL_HOLDER.position, speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        ResetBall();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum BrickType {
    OSCILLATING,
    NONOSCILLATING,
    BOMB
}

// CONVERT IT TO 3 BRICK TYPES

public class SquareBrickController : MonoBehaviour
{
    SpriteRenderer sr;
    public BrickType brickType;
    [SerializeField] SquareBrickController[] neighbourBricks;
    [SerializeField] float oscillate_dist;
    [SerializeField] bool isOscillatingLeft;
    [SerializeField] float blockSpeed;
    [SerializeField] int hits;
    [SerializeField] SpriteRenderer outlineSR;
    [SerializeField] SpriteRenderer darkerOutlineSR;
    [SerializeField] TextMeshPro brickText;
    [SerializeField] LevelTracker levelTracker;
    ParticleSystem explosionPS;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        if (brickType == BrickType.OSCILLATING) {
            brickText.text = hits.ToString();
            darkerOutlineSR.color = Color.blue;
            outlineSR.color = new Color(0.706f, 0.706f, 1f);
        } else if (brickType == BrickType.NONOSCILLATING) {
            brickText.text = hits.ToString();
            darkerOutlineSR.color = Color.green;
            outlineSR.color = new Color(0.706f, 1f, 0.706f);
        }    
    }

    private void Start() {
        explosionPS = levelTracker.GetExplosionPSPrefab(brickType);
        if (brickType == BrickType.OSCILLATING) {
            StartCoroutine(OscillateBrick(oscillate_dist, isOscillatingLeft));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hits <= 0) {
            levelTracker.DecreaseBrickCount();
            DisableBrick();
        } 
        if (brickType != BrickType.BOMB) 
            brickText.text = hits.ToString();
    }

    public void DisableBrick() {
        Instantiate(explosionPS, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        for (int i = 0; i < neighbourBricks.Length; i++) {
            levelTracker.DecreaseBrickCount();
            neighbourBricks[i].DisableBrick();
        }
    }

    public void decreaseHeight() {
        transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0f);
    }

    IEnumerator DisplayColor() {
        if (brickType == BrickType.OSCILLATING) {
            outlineSR.color = new Color(0.9f, 0.9f, 1f);
        } else if (brickType == BrickType.NONOSCILLATING) {
            outlineSR.color = new Color(0.9f, 1f, 0.9f);
        }
        yield return new WaitForSeconds(0.1f);
        if (brickType == BrickType.OSCILLATING) {
            outlineSR.color = new Color(0.706f, 0.706f, 1f);
        } else if (brickType == BrickType.NONOSCILLATING) {
            outlineSR.color = new Color(0.706f, 1f, 0.706f);
        }
    }

    IEnumerator OscillateBrick(float dist, bool isGoingLeft) {
        
        Vector2 leftTarget = new Vector2(transform.position.x - dist, transform.position.y);
        Vector2 rightTarget = new Vector2(transform.position.x + dist, transform.position.y);
        while (true) {
            while (isGoingLeft && transform.position.x >= leftTarget.x) {
                if (!levelTracker.isGamePaused) {
                    Vector3 pos = transform.position;
                    pos.x = pos.x - blockSpeed;
                    transform.position = pos;
                    yield return new WaitForFixedUpdate();
                }
            }
            isGoingLeft = false;
            while (!isGoingLeft && transform.position.x <= rightTarget.x) {
                if (!levelTracker.isGamePaused) {
                    Vector3 pos = transform.position;
                    pos.x = pos.x + blockSpeed;
                    transform.position = pos;
                    yield return new WaitForFixedUpdate();
                }
            }
            isGoingLeft = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ball")) {
            hits -= 1;
            StartCoroutine(DisplayColor());
        } else if (other.gameObject.CompareTag("Game-Over")) {
            Debug.Log("Game Over Now.");
            levelTracker.isGameOver = true;
        }
    }
}

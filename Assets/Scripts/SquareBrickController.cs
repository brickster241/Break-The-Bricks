using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SquareBrickController : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField] bool isOscillating = false;
    [SerializeField] float oscillate_dist;
    [SerializeField] bool isOscillatingLeft;
    [SerializeField] float blockSpeed;
    [SerializeField] int hits;
    [SerializeField] SpriteRenderer outlineSR;
    [SerializeField] SpriteRenderer darkerOutlineSR;
    [SerializeField] TextMeshPro brickText;
    [SerializeField] LevelTracker levelTracker;

    private void Awake() {
        brickText.text = hits.ToString();
        sr = GetComponent<SpriteRenderer>();
        darkerOutlineSR.color = (!isOscillating) ? Color.green : Color.blue;
        outlineSR.color = (!isOscillating) ? new Color(0.706f, 1f, 0.706f) : new Color(0.706f, 0.706f, 1f);    
    }

    private void Start() {
        if (isOscillating) {
            StartCoroutine(OscillateBrick(oscillate_dist, isOscillatingLeft));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hits <= 0) {
            levelTracker.DecreaseBrickCount();
            gameObject.SetActive(false);
        }
            
        brickText.text = hits.ToString();
    }

    public void decreaseHeight() {
        transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0f);
    }

    IEnumerator DisplayColor() {
        outlineSR.color = (!isOscillating) ? new Color(0.9f, 1f, 0.9f) : new Color(0.9f, 0.9f, 1f);
        yield return new WaitForSeconds(0.1f);
        outlineSR.color = (!isOscillating) ? new Color(0.706f, 1f, 0.706f) : new Color(0.706f, 0.706f, 1f);   
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SquareBrickController : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField] bool isOscillating = false;
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

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ball")) {
            hits -= 1;
            StartCoroutine(DisplayColor());
        } else if (other.gameObject.CompareTag("Game-Over")) {
            levelTracker.isGameOver = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SquareBrickController : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField] int hits;
    [SerializeField] SpriteRenderer outlineSR;
    [SerializeField] TextMeshPro brickText;

    private void Awake() {
        brickText.text = hits.ToString();
        sr = GetComponent<SpriteRenderer>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hits == 0)
            Destroy(gameObject);
        brickText.text = hits.ToString();
    }

    IEnumerator DisplayColor() {
        Color original = outlineSR.color;
        Color temp = original;
        temp.r = 0.9f;
        temp.b = 0.9f;
        outlineSR.color = temp;
        yield return new WaitForSeconds(0.2f);
        outlineSR.color = original;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ball")) {
            hits -= 1;
            StartCoroutine(DisplayColor());
        }
    }
}

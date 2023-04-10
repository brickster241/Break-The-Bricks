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
        
    }
}

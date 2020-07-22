using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }
    void OnMouseOver() {
        sr.color = Color.gray;
    }

    void OnMouseExit() {
        sr.color = new Color(255,255,255,200);
    }

}

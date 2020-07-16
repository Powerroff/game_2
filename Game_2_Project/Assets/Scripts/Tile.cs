using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Global Objects
    GameManager gm;
    BoardManager bm;

    //Personal Vars
    SpriteRenderer sr;





    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        bm = gm.bm;
        sr = GetComponent<SpriteRenderer>();
    }

    public void setSprite(Sprite s) {
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        sr.sprite = s;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

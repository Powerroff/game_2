using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Global Objects
    GameManager gm;
    BoardManager bm;

    //Personal Vars





    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        bm = gm.bm;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

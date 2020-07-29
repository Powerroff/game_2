using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public partial class BoardManager : MonoBehaviour
{


    
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Gets tile info at point, and generates it if not already present.
    TileInfo getTile(Vector2Int loc) {
        if (tileDict.ContainsKey(loc))
            return tileDict[loc];
        else {
            TileInfo t = bioM.getBiome(loc).genTile();
            tileDict.Add(loc, t);
            t.instantiate(loc, transform);
            return t;
        }
    }



    public void renderBoard() {
        for (int i = -30; i <= 30; i++) {
            for (int j = 0; j <= 30; j++) {
                getTile(new Vector2Int(i, j));
            }
        }
        for (int radius = 0; radius <= RENDER_RADIUS; radius++)
            foreach (Vector2Int loc in atRadius(Vector2Int.zero, radius))
                getTile(loc).display(true);

    }



}

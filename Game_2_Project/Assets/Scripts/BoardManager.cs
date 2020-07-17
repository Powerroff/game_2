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
            return t;
        }
    }

    public void renderBoard() {
        for (int i = -30; i <= 30; i++) {
            for (int j = -30; j <= 30; j++) {
                Vector2Int loc = new Vector2Int(i, j);
                GameObject t = Instantiate(tilePrefab, transform);
                t.transform.SetPositionAndRotation((Vector2)loc, Quaternion.identity);
                t.GetComponent<Tile>().setSprite(getTile(loc).sprite);
            }
        }
    }



}

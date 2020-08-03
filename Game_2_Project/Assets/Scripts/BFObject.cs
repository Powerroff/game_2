using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFObject : MonoBehaviour
{

    Battlefield bf;
    int col;
    int row;

    void Awake() {
        bf = GameManager.instance.bm.battlefield;
    }

    public bool moveTo(int col, int row) {
        if (col < 0 || col > 2) return false;
        if (row < 0 || row > 8 - col % 2) return false; //columns have heights 8, 7, 8

        transform.localPosition = bf.getLoc(col, row);
        this.col = col;
        this.row = row;
        return true;
    }

    public void moveToBounded(int col, int row) {
        col = Mathf.Clamp(col, 0, 2);
        row = Mathf.Clamp(row, 0, 8 - col % 2);
        moveTo(col, row);
    }

    public void moveUpBounded(int d) {
        moveToBounded(col, row - d);
    }


}

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

    public void moveToHex(Hex h) {
        moveTo(h.col, h.row);
    }

    public bool canMoveTo(Hex h) {
        return false;
        
        
        //switch (col) {
     //       case 0:
     //       case 2:
     //           return (h.col == 1 && h.row == Mathf.Clamp(h.row, row - 1, row)) || (h.col == col && Mathf.Abs(h.row - row) == 1);
     //      case 1:
     //       default:
     //          return (h.col != col && h.row == Mathf.Clamp(h.row, row, row + 1)) || (h.col == col && Mathf.Abs(h.row - row) == 1);
     //   }

    }


}

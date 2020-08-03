using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{

    public Hex[] Lhexes;
    public Hex[] Chexes;
    public Hex[] Rhexes;
    
    public Vector2 getLoc(int col, int row) {
        if (col == 0 || col == 2)
            return new Vector2(col + 7f, (4 - row) * 1.25f);
        if (col == 1)
            return new Vector2(col + 7f, (4 - row - .5f) * 1.25f);
        Debug.Log("Battlefield index out of bounds");
        return Vector2.zero;
    }

    



}

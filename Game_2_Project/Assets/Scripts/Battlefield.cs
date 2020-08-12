using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{

    public Hex[] Lhexes;
    public Hex[] Chexes;
    public Hex[] Rhexes;

    Dictionary<Hex, BFObject> bfObjects;

    Hex selected = null;

    public Vector2 getLoc(int col, int row) {
        if (col == 0 || col == 2)
            return new Vector2(col + 7f, (4 - row) * 1.25f);
        if (col == 1)
            return new Vector2(col + 7f, (4 - row - .5f) * 1.25f);
        Debug.Log("Battlefield index out of bounds");
        return Vector2.zero;
    }

    void Awake() {
        bfObjects = new Dictionary<Hex, BFObject>();
    }

    public void addObject(Hex hex, BFObject bfObject) {
        bfObjects.Add(hex, bfObject);
    }

    public Hex getHex(int col, int row) {
        switch (col) {
            case 0:
                return Lhexes[row];
            case 1:
                return Chexes[row];
            case 2:
            default:
                return Rhexes[row];
        }
    }

    public void UpdatePosition(Hex h1, Hex h2) {
        if (bfObjects.ContainsKey(h1) && !bfObjects.ContainsKey(h2)) {
            bfObjects.Add(h2, bfObjects[h1]);
            bfObjects.Remove(h1);
        }
    }

    public void onHexPress(Hex h) {
        //Toggles selected hex
        if (selected != null) {
            AttemptSwitch(selected, h);
            selected = null;
        } else selected = h;
    }

    void AttemptSwitch(Hex h1, Hex h2) {
        if (bfObjects.ContainsKey(h1) && bfObjects[h1].canMoveTo(h2)) {
            bfObjects[h1].moveToHex(h2);
            UpdatePosition(h1, h2);
        }
    }





}

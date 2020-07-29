using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public partial class BoardManager : MonoBehaviour
{
    public GameObject partyPrefab;

    Vector2Int partyLoc;
    Party party;

    const int RENDER_RADIUS = 5;

    public void initParty() {
        UnityEngine.Debug.Assert(party == null);
        party = Instantiate(partyPrefab, transform).GetComponent<Party>();
        partyLoc = Vector2Int.zero;

    }

    public void moveTo(Vector2Int loc) {
        if ((loc - partyLoc).sqrMagnitude == 1) {
            party.moveTo(loc);
            partyLoc = loc;

            //This is inefficient but w/e
            foreach (Vector2Int l in atRadius(partyLoc, RENDER_RADIUS))
                getTile(l).display(true);
            foreach (Vector2Int l in atRadius(partyLoc, RENDER_RADIUS + 1))
                getTile(l).display(false);
        }

    }

    static List<Vector2Int> atRadius(Vector2Int center, int radius) {
        List<Vector2Int> l = new List<Vector2Int>();
        for (int i = -radius; i < radius; i++) {
            l.Add(center + new Vector2Int(radius, i));
            l.Add(center + new Vector2Int(-radius, -i));
            l.Add(center + new Vector2Int(i, -radius));
            l.Add(center + new Vector2Int(-i, radius));
        }
        return l;


    }





}

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public partial class BoardManager : MonoBehaviour
{
    public GameObject partyPrefab;

    Vector2Int partyLoc;
    Party party;

    public void initParty() {
        UnityEngine.Debug.Assert(party == null);
        party = Instantiate(partyPrefab, transform).GetComponent<Party>();
        partyLoc = Vector2Int.zero;

    }

    public void moveTo(Vector2Int loc) {
        if ((loc - partyLoc).sqrMagnitude == 1) {
            party.moveTo(loc);
            partyLoc = loc;
        }

    }




}

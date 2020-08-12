using UnityEngine;

public partial class BoardManager : MonoBehaviour
{
    public GameObject hexPrefab;
    public GameObject battlefieldPrefab;
    public GameObject bfPartyPrefab;

    public Battlefield battlefield;

    BFObject bfParty;


    public void initBattlefield() {
        battlefield = Instantiate(battlefieldPrefab, party.transform).GetComponent<Battlefield>();
        bfParty = Instantiate(bfPartyPrefab, battlefield.transform).GetComponent<BFObject>();
        bfParty.moveTo(1, 4);
        battlefield.addObject(battlefield.getHex(1, 0), bfParty);
    }

}

using UnityEngine;

public partial class BoardManager : MonoBehaviour
{
    public GameObject hexPrefab;
    public GameObject battlefield;

    const int BF_SEMILENGTH = 4;


    public void initBattlefield() {
        battlefield.transform.SetParent(party.transform);
        for (int i = 0; i < 2 * BF_SEMILENGTH + 1; i++) {
            GameObject h = Instantiate(hexPrefab, battlefield.transform);
            h.transform.SetPositionAndRotation(new Vector2(7, (i - BF_SEMILENGTH)*1.25f), Quaternion.identity);

            h = Instantiate(hexPrefab, battlefield.transform);
            h.transform.SetPositionAndRotation(new Vector2(9, (i - BF_SEMILENGTH) * 1.25f), Quaternion.identity);
        }
        for (int i = 0; i < 2 * BF_SEMILENGTH; i++) {
            GameObject h = Instantiate(hexPrefab, battlefield.transform);
            h.transform.SetPositionAndRotation(new Vector2(8, (i - BF_SEMILENGTH + 0.5f) * 1.25f), Quaternion.identity);
        }
    }

}

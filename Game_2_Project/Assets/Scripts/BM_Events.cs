using TMPro;
using UnityEngine;

public partial class BoardManager : MonoBehaviour
{

    public GameObject canvas;
    public TextMeshProUGUI eventTitle;
    public TextMeshProUGUI eventText;

    public void executeTileEvents(Tile t) {
        eventTitle.text = t.name;
    }

}

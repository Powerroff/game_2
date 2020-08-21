using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public partial class BoardManager
{
    public TextMeshProUGUI stamText, woodText, scrapText;
    int stam, wood, scrap;

    //public enum Resources { stam, wood, scrap };

    void updateResources() {
        stamText.text = "Stamina: " + stam;
        woodText.text = "Wood: " + wood;
        scrapText.text = "Scrap: " + scrap;
    }

    public void addResources(int stamLoss, int woodGain, int scrapGain) {
        stam -= stamLoss;
        wood += woodGain;
        scrap += scrapGain;
        updateResources();
    }
}

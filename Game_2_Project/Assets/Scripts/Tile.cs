using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Global Objects
    GameManager gm;
    BoardManager bm;

    //Prefab Vars
    public Sprite clearedSprite;
    public string name;
    public string description;
    public string unclearedText;
    public string clearedText;
    public int stamCost;
    public int wood;
    public int scrap;

    //Personal Vars
    public bool explored = false;
    public bool reachable = false;
    Vector2Int loc;
    int fogDistance_sq = 16;
    SpriteRenderer sr;
    Color tempColor = Color.black;

    public static Tile InstantiateTile(GameObject prefab, Transform parent, Vector2Int loc) {
        GameObject g = Instantiate(prefab, parent);
        g.transform.SetPositionAndRotation((Vector2)loc, Quaternion.identity);
        g.SetActive(false);
        Tile t = g.GetComponent<Tile>();
        t.loc = loc;
        t.setBrightness(16);
        return t;
    }

    public void updateDist(Vector2Int partyLoc) {
        int dist_sq = (partyLoc - loc).sqrMagnitude;
        fogDistance_sq = Math.Min(fogDistance_sq, dist_sq);

        reachable = (dist_sq == 1);
        setBrightness(Math.Min(fogDistance_sq + 12, dist_sq));
    }

    void setBrightness(int level) {
        float darkness = (float)(1.5 - Math.Sqrt(level) / 2.5);
        sr.color = new Color(darkness, darkness, darkness);
    }



    public void setActive(bool active) {
        gameObject.SetActive(active);
    }


    public void setSprite(Sprite s) {
        sr.sprite = s;
    }

    public void explore() {
        sr.sprite = clearedSprite;
        explored = true;
    }

    void OnMouseExit() {
        sr.color = tempColor;
    }

    private void OnMouseEnter() {
        tempColor = sr.color;
        if (reachable)
            sr.color = Color.gray;
    }

    void OnMouseDown() {
        if (reachable) {
            bm.moveTo(Vector2Int.RoundToInt((Vector2)transform.position));
            bm.executeTileEvents(this);
            if (!explored)
                explore();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void Awake() {
        gm = GameManager.instance;
        bm = gm.bm;
        sr = GetComponent<SpriteRenderer>();
    }
}

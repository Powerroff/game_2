using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Global Objects
    GameManager gm;
    BoardManager bm;

    //Prefab Vars
    public Sprite clearedSprite;
    public String name;
    public String description;

    //Personal Vars
    public bool explored = false;
    public bool reachable = false;
    Vector2Int loc;
    int fogDistance_sq = 16;
    SpriteRenderer sr;

    public static Tile InstantiateTile(GameObject prefab, Transform parent, Vector2Int loc) {
        GameObject g = Instantiate(prefab, parent);
        g.transform.SetPositionAndRotation((Vector2)loc, Quaternion.identity);
        g.SetActive(false);
        Tile t = g.GetComponent<Tile>();
        t.loc = loc;
        t.setFog();
        return t;
    }

    public void updateDist(Vector2Int partyLoc) {
        int dist_sq = (partyLoc - loc).sqrMagnitude;
        fogDistance_sq = Math.Min(fogDistance_sq, dist_sq);

        reachable = (dist_sq == 1);
        setFog();
    }

    void setFog() {
        float darkness = (float)(1.5 - Math.Sqrt(fogDistance_sq) / 2.5);
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


    void OnMouseOver() {
        if (reachable)
            sr.color = Color.gray;
    }

    void OnMouseExit() {
        setFog();
    }

    void OnMouseDown() {
        if (reachable) {
            if (!explored)
                explore();
            bm.moveTo(Vector2Int.RoundToInt((Vector2)transform.position));
            bm.executeTileEvents(this);
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

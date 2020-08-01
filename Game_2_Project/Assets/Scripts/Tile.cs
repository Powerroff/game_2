using UnityEngine;

public class Tile : MonoBehaviour
{
    //Global Objects
    GameManager gm;
    BoardManager bm;

    //Personal Vars
    SpriteRenderer sr;
    public Sprite clearedSprite;
    public bool explored = false;
    public bool reachable = false;
    
    public static Tile InstantiateTile(GameObject prefab, Transform parent, Vector2 loc) {
        GameObject g = Instantiate(prefab, parent);
        g.transform.SetPositionAndRotation(loc, Quaternion.identity);
        g.SetActive(false);
        return g.GetComponent<Tile>();
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
        sr.color = Color.white;
    }

    void OnMouseDown() {
        if (reachable) {
            if (!explored)
                explore();
            bm.moveTo(Vector2Int.RoundToInt((Vector2)transform.position));
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

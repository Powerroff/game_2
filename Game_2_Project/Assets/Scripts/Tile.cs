using UnityEngine;

public class Tile : MonoBehaviour
{
    //Global Objects
    GameManager gm;
    BoardManager bm;

    //Personal Vars
    SpriteRenderer sr;







    public void setSprite(Sprite s) {
        sr.sprite = s;
    }

    void OnMouseOver() {
        sr.color = Color.gray;
    }

    void OnMouseExit() {
        sr.color = Color.white;
    }

    void OnMouseDown() {
        bm.moveTo(Vector2Int.RoundToInt((Vector2)transform.position));
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

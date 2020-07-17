using UnityEngine;

public class Tile : MonoBehaviour
{
    //Global Objects
    GameManager gm;
    BoardManager bm;

    //Personal Vars
    SpriteRenderer sr;





    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        bm = gm.bm;
        sr = GetComponent<SpriteRenderer>();
    }

    public void setSprite(Sprite s) {
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        sr.sprite = s;
    }

    void OnMouseOver() {
        Debug.Log("MO");
        sr.color = Color.gray;
    }

    void OnMouseExit() {
        sr.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

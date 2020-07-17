using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public void moveTo(Vector2Int loc) {
        transform.SetPositionAndRotation((Vector2)loc, Quaternion.identity);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

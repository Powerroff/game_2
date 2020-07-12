using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    
    


    void Start() {

    }

    void Update() {

    }




    //Static instance of GameManager which allows it to be accessed by any other script.
    public static GameManager instance = null;
    void Awake() {
        Debug.Log("awoke");

        //Check if instance already exists
        if (instance == null) {
            Debug.Log("Instantiating gm");
            //if not, set instance to this
            instance = this;
        }

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, 
            // meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene / Switching scenes
        DontDestroyOnLoad(gameObject); // VERY IMPORTANT

    }
    //^^ Above section creates a static GameManager

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public RuntimeVar Health;
    public static GameHandler Handler;
    public Transform pSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Handler == null)
            Handler = FindObjectOfType(typeof(GameHandler)) as GameHandler;
    }
    
    void Update() {
        if (Health.RuntimeVal == 0){
            Debug.Log("Health is 0, trying to update");
            Handler.Died();
            Health.RuntimeVal += 1 /*TODO: add whatever we want to reset their lives to be here */; 
        }
    }

    public void OnCollisionEnter2D(Collision2D Collision) {
        if(Collision.gameObject.tag == "Enemy") {
            Debug.Log("player health was " + Health.RuntimeVal);
            Health.RuntimeVal -= 2;
            Debug.Log("player health is now " + Health.RuntimeVal);
        }
    }

}

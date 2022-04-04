using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public RuntimeVar Health;
    
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnCollisionEnter2D(Collision2D Collision) {
        Debug.Log("collision occurred with the " + Collision.gameObject.tag);
        if(Collision.gameObject.tag == "Enemy") {
            Debug.Log("player health was " + Health.RuntimeVal);
            Health.RuntimeVal -= 5;
            Debug.Log("player health is now " + Health.RuntimeVal);
            
            if (Health.RuntimeVal == 0)
                Died();
        }
    }
}

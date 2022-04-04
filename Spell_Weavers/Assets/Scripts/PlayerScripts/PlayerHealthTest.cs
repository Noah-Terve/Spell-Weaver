using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthTest : MonoBehaviour
{
    // Start is called before the first frame update
    public RuntimeVar PlayerHealth;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D Collision) {
        Debug.Log("collision occurred");
        if(Collision.gameObject.tag == "Enemy") {
            Debug.Log("player health was " + PlayerHealth.RuntimeVal);
            PlayerHealth.RuntimeVal -= 5;
            Debug.Log("player health is now " + PlayerHealth.RuntimeVal);
        }
    }
}

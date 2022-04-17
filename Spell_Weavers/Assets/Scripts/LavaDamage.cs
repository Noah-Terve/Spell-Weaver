using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public RuntimeVar PlayerHp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            PlayerHp.RuntimeVal = 0;
            Debug.Log("Lava Killed Player");
        }
    }

}

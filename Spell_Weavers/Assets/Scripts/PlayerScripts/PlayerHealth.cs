using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public RuntimeVar Health;
    public static GameHandler Handler;
    public Animator animator;
    public Transform pSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();

        if (Handler == null)
            Handler = FindObjectOfType(typeof(GameHandler)) as GameHandler;
    }
    
    void Update() {
        if (transform.position.y < -50)
            Handler.Died();
            
        if (Health.RuntimeVal == 0){
            Debug.Log("Health is 0, trying to respawn");
            Handler.Died();
            Health.RuntimeVal += 20 
            /*TODO: add whatever we want to reset their lives to be here */;

        }
    }

    public void OnCollisionEnter2D(Collision2D Collision) {
        if(Collision.gameObject.tag == "Enemy") {
            animator.SetTrigger("Hit");
            Debug.Log("player health was " + Health.RuntimeVal);
            Health.RuntimeVal -= 2;
            Debug.Log("player health is now " + Health.RuntimeVal);
        }
    }

}

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
            Handler.Died();
            Health.RuntimeVal += 20;
        }
    }
}

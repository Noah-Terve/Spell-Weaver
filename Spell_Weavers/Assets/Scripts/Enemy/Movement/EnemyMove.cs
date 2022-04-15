using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMove : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected int groundLayer;
    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");
    }


    public abstract void idle();

    public abstract void attackPlayer(GameObject player);

}

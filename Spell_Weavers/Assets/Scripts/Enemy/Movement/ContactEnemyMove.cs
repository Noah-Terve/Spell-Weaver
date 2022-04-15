using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactEnemyMove : EnemyMove
{
    public float speed = 5f;
    public float jumpPower = 5f;


    bool goingRight = true;
    

    public override void idle() {
        Vector2 pos = transform.position;
        
        Collider2D[] airDetector = Physics2D.OverlapCircleAll(pos + (Vector2.down * 0.5f), 0.25f, groundLayer); 
        if (airDetector.Length == 0)
            return;

        Collider2D[] platformDetecter; 

        if (goingRight) 
            platformDetecter = Physics2D.OverlapCircleAll(pos + (Vector2.right * 0.5f) + (Vector2.down * 0.5f), 0.25f, groundLayer);
        else 
            platformDetecter = Physics2D.OverlapCircleAll(pos + (Vector2.left * 0.5f) + (Vector2.down * 0.5f), 0.25f, groundLayer);
        
        if (platformDetecter.Length == 0)
            goingRight = !goingRight;

        
        if (goingRight)
            rb.velocity = Vector2.right * speed;
        else 
            rb.velocity = Vector2.left * speed;
    }

    public override void attackPlayer(GameObject player) {

    }
}

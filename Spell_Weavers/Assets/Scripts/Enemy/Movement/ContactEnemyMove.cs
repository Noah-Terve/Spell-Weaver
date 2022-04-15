using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactEnemyMove : EnemyMove
{
    public float speed = 5f;
    public float jumpPower = 5f;
    public float jumpWithinRange = 0.5f;

    bool onGround = false;
    bool goingRight = true;
    

    public override void idle() {
        Vector2 pos = transform.position;
        
        Collider2D[] airDetector = Physics2D.OverlapCircleAll(pos + (Vector2.down * 0.5f), 0.25f, groundLayer); 
        onGround = airDetector.Length != 0;

        if (!onGround)
            return;

        Collider2D[] platformDetecter; 


        // Choose the dircetion to detect
        if (goingRight) 
            platformDetecter = Physics2D.OverlapCircleAll(pos + (Vector2.right * 0.5f) + (Vector2.down * 0.5f), 0.25f, groundLayer);
        else 
            platformDetecter = Physics2D.OverlapCircleAll(pos + (Vector2.left * 0.5f) + (Vector2.down * 0.5f), 0.25f, groundLayer);
        
        // Turn when at the edge of a platform
        if (platformDetecter.Length == 0)
            goingRight = !goingRight;

        
        // Choose the movement
        if (goingRight)
            rb.velocity = Vector2.right * speed;
        else 
            rb.velocity = Vector2.left * speed;
    }

    public override void attackPlayer(GameObject player) {
        Vector2 vel = rb.velocity;
        if (player.transform.position.x > transform.position.x)
            vel.x = speed;
        else
            vel.x = -speed;
        
        rb.velocity = vel;
        // If within range to jump and on the ground, jump
        if (onGround && Mathf.Abs(player.transform.position.x - transform.position.x) < jumpWithinRange && player.transform.position.y - transform.position.y > 0) {
            onGround = false;
            rb.velocity += Vector2.up * jumpPower;
        } 
    }
}

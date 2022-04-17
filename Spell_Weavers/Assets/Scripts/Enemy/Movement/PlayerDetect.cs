using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{

    public float lookDistance = 5f;
    public float forgetTime = 10f;
    public bool isHit = false;
    public float DistanceToPlayer = 1.5f;

    bool foundPlayer = false;
    GameObject player;
    int layerMask;
    float currTime = 0f;
    EnemyMove enemyAI;
    
    protected Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        layerMask = ~LayerMask.GetMask("Enemy");
        enemyAI = GetComponent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        // Can't react if hit
        if (isHit)
            return;
        
        // If the enemy is close to the player it stops trying to get closer
        // TODO: then the enemy tries to attack the player (idk, jumps up and down and then deals dmg)
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < DistanceToPlayer){
            rb.velocity = new Vector2(0f, rb.velocity.y);
            enemyAI.idle();
            return;
        }
        
        // Look left or right (in direction of player)
        RaycastHit2D hit;
        if (player.transform.position.x > transform.position.x)
            hit = Physics2D.Raycast(transform.position, Vector2.right, lookDistance, layerMask);
        else
            hit = Physics2D.Raycast(transform.position, Vector2.left, lookDistance, layerMask);

        // Set player to found
        if (hit.collider != null && hit.collider.gameObject.tag == "Player") {
            foundPlayer = true;
            currTime = Time.time;
        }

        // Forget about the player after a specified time
        // if (Time.time > currTime + forgetTime) 
        //     foundPlayer = false;
        
        
        // Activates the current behavior
        if (foundPlayer) // What to do when tracking
            enemyAI.attackPlayer(player);
        else // What to do when not tracking
            enemyAI.idle();
    }
}

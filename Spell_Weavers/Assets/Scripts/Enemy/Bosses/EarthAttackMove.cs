using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthAttackMove : MonoBehaviour
{
    // Start is called before the first frame update
public GameHandler gameHandlerObj;
       public int damage = 5;
       public float speed = 10f;
       private Transform playerTrans;
       private Vector2 target;
       public GameObject hitEffectAnim;
       public float SelfDestructTime = 2.0f;

       public float knockBack = 10f;

       public GameObject vfx;

       public RuntimeVar PlayerHP;

       void Start() {
             //NOTE: transform gets location, but we need Vector2 for direction, so we can use MoveTowards.
            //  playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
            //  target = new Vector2(playerTrans.position.x, playerTrans.position.y);

            //  if (gameHandlerObj == null){
            //    gameHandlerObj = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
            //  }
            //  StartCoroutine(selfDestruct());
       }

       void Update () {
            //   transform.position = Vector2.MoveTowards (transform.position, target, speed * Time.deltaTime);
       }

       //if the bullet hits a collider, play the explosion animation, then destroy the effect and the bullet
       void OnCollisionEnter2D(Collision2D hit){
        if (hit.gameObject.tag != "Player") {
            destroySelf();
            return;
        }
        // MAY WANT TO CHANGE HOW IT WORKS
        GameObject player = hit.gameObject;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>(); 
        float accel = player.GetComponent<PlayerMove>().accelerate;
        
        float pushBack = 0f;
            if (player.transform.position.x > gameObject.transform.position.x)
                pushBack = knockBack;
            else 
                pushBack = -knockBack;
            pushBack -= rb.velocity.x;
        rb.AddForce(rb.mass * (Vector2.right * pushBack * accel), ForceMode2D.Impulse);
        PlayerHP.RuntimeVal -= damage;

        destroySelf();
       }

    void destroySelf() {
        if (vfx != null) {
            Instantiate(vfx, transform.position, vfx.transform.rotation);
        }
        Destroy(gameObject);
    }
}

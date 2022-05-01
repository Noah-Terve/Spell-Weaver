using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveHit : MonoBehaviour
{
    public Animator anim;
       public float speed = 4f;
       private Transform target;
       public int damage = 10;
       public float knockBack = 20f;

       public int EnemyLives = 3;
       private GameHandler gameHandler;

       public float attackRange = 10;
       public bool isAttacking = false;
       private float scaleX;

       void Start () {
              anim = GetComponentInChildren<Animator> ();
              scaleX = gameObject.transform.localScale.x;

              if (GameObject.FindGameObjectWithTag ("Player") != null) {
                     target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
              }

              if (GameObject.FindWithTag ("GameController") != null) {
                  gameHandler = GameObject.FindWithTag ("GameController").GetComponent<GameHandler> ();
              }
       }

       void Update () {
              float DistToPlayer = Vector3.Distance(transform.position, target.position);

              if ((target != null) && (DistToPlayer <= attackRange)){
                     transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
                     if (isAttacking == false) {
                            //anim.SetBool("Walk", true);
                            //flip enemy to face player direction. Wrong direction? Swap the * -1.
                            if (target.position.x > gameObject.transform.position.x){
                                   gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
                            } else {
                                    gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
                            }
                     }
                     //else  { anim.SetBool("Walk", false);}
              }
               //else { anim.SetBool("Walk", false);}
       }

       public void OnCollisionExit2D(Collision2D collision){
              if (collision.gameObject.tag == "Player") {
                     isAttacking = false;
                     //anim.SetBool("Attack", false);
              }
       }

       //DISPLAY the range of enemy's attack when selected in the Editor
       void OnDrawGizmosSelected(){
              Gizmos.DrawWireSphere(transform.position, attackRange);
       }
       
       public void OnCollisionEnter2D(Collision2D collision) {
              if (collision.gameObject.tag != "Player")
                     return;
              
              GameObject player = collision.gameObject;
              isAttacking = true;
              
              //anim.SetBool("Attack", true);
              // TODO: turn back on when gamehandler deals with player hp
              // gameHandler.playerGetHit(damage);
              //rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
              //StartCoroutine(HitEnemy());

              float pushBack = 0f;
              if (player.transform.position.x > gameObject.transform.position.x)
                     pushBack = knockBack;
              else 
                     pushBack = -knockBack;
              
              // collision.gameObject.transform.position = new Vector3(transform.position.x + pushBack, transform.position.y + 1, -1);
              // collision.gameObject.GetComponent<Rigidbody2D>().velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity + Vector2.right * knockBack;
              player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * knockBack, ForceMode2D.Impulse);
      }
}

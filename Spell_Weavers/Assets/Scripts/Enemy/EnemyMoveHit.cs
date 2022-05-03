using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveHit : MonoBehaviour
{
    public Animator anim;
       public float speed = 4f;
       private Transform target;
       public int damage = 2;
       public float knockBack = 20f;

       public int EnemyLives = 3;
       private GameHandler gameHandler;

       public float attackRange = 10;
       public bool isAttacking = false;
       private float scaleX;

       private Rigidbody2D rb;

       public static float noResponseTime = 0.114f;
       
       public RuntimeVar PlayerHealth;

       void Start () {
              anim = GetComponentInChildren<Animator> ();
              scaleX = gameObject.transform.localScale.x;

              rb = GetComponent<Rigidbody2D>();

              if (GameObject.FindGameObjectWithTag ("Player") != null) {
                     target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
              }

              if (GameObject.FindWithTag ("GameController") != null) {
                  gameHandler = GameObject.FindWithTag ("GameController").GetComponent<GameHandler> ();
              }
       }

       void Update () {
              float DistToPlayer = Vector3.Distance(transform.position, target.position);
              if ((target == null) || (DistToPlayer > attackRange))
                     return;

              // transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
              Vector2 toPlayer = target.position - transform.position;
              float dir = Mathf.Sign(toPlayer.x);
              float move = speed * dir - rb.velocity.x;

              rb.AddForce(Vector2.right * move);

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
              Rigidbody2D rb = player.GetComponent<Rigidbody2D>(); 
              isAttacking = true;
              
              //anim.SetBool("Attack", true);
              PlayerHealth.RuntimeVal -= damage;
              //rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
              //StartCoroutine(HitEnemy());

              float pushBack = 0f;
              if (player.transform.position.x > gameObject.transform.position.x)
                     pushBack = knockBack;
              else 
                     pushBack = -knockBack;
              pushBack -= rb.velocity.x;


              // collision.gameObject.transform.position = new Vector3(transform.position.x + pushBack, transform.position.y + 1, -1);
              // collision.gameObject.GetComponent<Rigidbody2D>().velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity + Vector2.right * knockBack;
              float accel = player.GetComponent<PlayerMove>().accelerate;
              //Vector2 dir = player.transform.position - transform.position;
              // dir.Normalize();
              rb.AddForce(rb.mass * (Vector2.right * pushBack * accel), ForceMode2D.Impulse);
      }

       IEnumerator playerPreventMove(PlayerMove playerMove) {
              playerMove.canMove = false;
              yield return new WaitForSeconds(noResponseTime);
              playerMove.canMove = true;
       }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{

      //public Animator anim;
        public float speed = 2f;
        public float stoppingDistance = 4f; // when enemy stops moving towards player
        public float retreatDistance = 3f; // when enemy moves away from approaching player
        private float timeBtwShots;
        public float startTimeBtwShots = 2;
        public List<GameObject> projectiles;

        private Rigidbody2D rb;
        private Transform player;
        private Vector2 PlayerVect;


    //    public int EnemyLives = 30;

    //    private Renderer rend;
       //private GameHandler gameHandler;

        public float attackRange = 10;
        public bool isAttacking = false;
        private float scaleX;
        public string ElemType = "Earth"; 

        public EnemyHP BossHP;

        public float originHP;

        public bool CanMove = true;
    //    public int CurrElemPhase = 0; 

        public void Start () {
            BossHP = GetComponent(typeof (EnemyHP)) as EnemyHP;
            originHP = BossHP.HP;
            Physics2D.queriesStartInColliders = false;
            scaleX = gameObject.transform.localScale.x;

            rb = GetComponent<Rigidbody2D> ();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            PlayerVect = player.transform.position;

            if (projectiles.Count == 0) {
                projectiles.Add(GameObject.Find(ElemType + "BossAttack"));
            }
            timeBtwShots = startTimeBtwShots;
       }

        public void Update () {

            float DistToPlayer = Vector3.Distance(transform.position, player.position);
            if ((player == null) || (DistToPlayer > attackRange) || isAttacking) {
                return;
            }
            // approach player
            if (Vector2.Distance (transform.position, player.position) > stoppingDistance) {
                transform.position = Vector2.MoveTowards (transform.position, player.position, speed * Time.deltaTime);
            }
                // stop moving
            else if (Vector2.Distance (transform.position, player.position) < stoppingDistance && Vector2.Distance (transform.position, player.position) > retreatDistance) {
                transform.position = this.transform.position;
                //anim.SetBool("Walk", false);
            }

                // retreat from player
            else if (Vector2.Distance (transform.position, player.position) < retreatDistance) {
                transform.position = Vector2.MoveTowards (transform.position, player.position, -speed * Time.deltaTime);
                if (isAttacking == false) {
                        //anim.SetBool("Walk", true);
                }
            }
                //Flip enemy to face player direction. Wrong direction? Swap the * -1.
            if (player.position.x > gameObject.transform.position.x) {
                gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
            } else {
                gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
            }

                //Timer for shooting projectiles
            if (timeBtwShots <= 0) {
                isAttacking = true;
                CanMove = false;
                //anim.SetTrigger("Attack");
                PerformAttack(player.position);
                // BasicAttack(player.position);
                timeBtwShots = startTimeBtwShots;
            } else {
                timeBtwShots -= Time.deltaTime;
                // isAttacking = false;
            }
    }

    void OnCollisionEnter2D(Collision2D collision) {
              //if (collision.gameObject.tag == "bullet") {
              // EnemyLives -= 1;
              // StopCoroutine("HitEnemy");
              // StartCoroutine("HitEnemy");
              //}
            //   if (collision.gameObject.tag == "Player") {
            //          EnemyLives -= 2;
            //          StopCoroutine("HitEnemy");
            //          StartCoroutine("HitEnemy");
            //   }
    }

    //    IEnumerator HitEnemy(){
    //           // color values are R, G, B, and alpha, each divided by 100
    //           rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
    //           if (EnemyLives < 1){
    //                  //gameControllerObj.AddScore (5);
    //                  Destroy(gameObject);
    //           }
    //           else yield return new WaitForSeconds(0.5f);
    //           rend.material.color = Color.white;
    //    }

      //DISPLAY the range of enemy's attack when selected in the Editor
    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public virtual void PerformAttack(Vector3 player) {}
    public virtual void BasicAttack(Vector3 player, float SizeMod) {}

    public virtual void DesperationAttack(Vector3 player) {}
}
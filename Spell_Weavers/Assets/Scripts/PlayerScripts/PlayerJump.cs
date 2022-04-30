using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    public Animator animator;
    public Rigidbody2D rb;
    public float jumpForce = 10f;
    public Transform feet;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public bool isAlive = true;
    public float coyoteTime = 0.12f;
    public float earlyJumpTime = 0.12f;

    float jumpTimer = 0f;
    float groundTimer = 0f;

    //public AudioSource JumpSFX;

    void Start(){
        animator = gameObject.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        jumpTimer -= Time.deltaTime;
        groundTimer -= Time.deltaTime;
        IsGrounded();
        if (Input.GetButtonDown("Jump"))
            jumpTimer = coyoteTime;
        
        if (isAlive && jumpTimer > 0 && groundTimer > 0) {
            Jump();
        }
    }
    
    public void OnCollisionEnter2D(Collision2D Collision) {
        if(Collision.gameObject.tag == "Ground") {
            animator.SetTrigger("Grounded");
        }
    }

    public void Jump() {
        float targetSpeed = jumpForce - rb.velocity.y;
        rb.AddForce(Vector2.up * targetSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
        groundTimer = 0;
        // Vector2 vel = rb.velocity;
        // vel.y = jumpForce;
        // rb.velocity = vel;
        animator.SetTrigger("Jump");
        // JumpSFX.Play();
    }

    public bool IsGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, .15f, groundLayer);
        Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, .05f, enemyLayer);
        if ((groundCheck != null) || (enemyCheck != null)) {
            groundTimer = earlyJumpTime;
            return true;
        }
        

        return false;
    }
}
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
    //public AudioSource JumpSFX;

    void Start(){
        animator = gameObject.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetButtonDown("Jump") && IsGrounded() && isAlive) {
            Jump();
            animator.SetTrigger("Jump");
            // JumpSFX.Play();
        }
    }
    
    public void OnCollisionEnter2D(Collision2D Collision) {
        if(Collision.gameObject.tag == "Ground") {
            animator.SetTrigger("Grounded");
        }
    }

    public void Jump() {
        rb.velocity = Vector2.up * jumpForce;
    }

    // TODO: Make sure to edit the overlap circles to make sure that the
    //       jump check works if you are on the edge.
    public bool IsGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, .05f, groundLayer);
        Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, .05f, enemyLayer);
        if ((groundCheck != null) || (enemyCheck != null)) {
            return true;
        }
        return false;
    }
}
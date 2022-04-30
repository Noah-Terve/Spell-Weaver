using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public Animator animator;
    public Rigidbody2D rb;
    public bool FaceRight = true; // determine which way player is facing.
    public static float runSpeed = 14f;
    public float startSpeed = 10f;
    public bool isAlive = true;

    public float accelerate = 15f;
    public float decelerate = 15f;
    public float speedDecay = 0.2f;
    public float velocityPwr = 1.1f;
    
    

    float input = 0f;

    public bool canMove = true;

    //public AudioSource WalkSFX;
    private Vector2 hMove = Vector2.zero;
    private Vector2 prevSpeed = Vector2.zero;
    void Start(){
        runSpeed = startSpeed;
        animator = gameObject.GetComponentInChildren<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
    }

    void Update() {
        //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
        
        // hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        // transform.position = transform.position + hMove * runSpeed * Time.deltaTime;
        /*
        prevSpeed = rb.velocity - hMove;
        prevSpeed = Vector2.Lerp(prevSpeed, Vector2.zero, Time.deltaTime);
        hMove = new Vector2(Input.GetAxis("Horizontal")  * runSpeed, 0.0f);
        rb.velocity = prevSpeed + hMove;
        */
        input = Input.GetAxis("Horizontal");
        

        if (Input.GetAxis("Horizontal") != 0){
              animator.SetBool ("Walk", true);
            //   if (!WalkSFX.isPlaying){
            //         WalkSFX.Play();
            //   }
        } else {
             animator.SetBool ("Walk", false);
            //  WalkSFX.Stop();
        }

        // NOTE: if input is moving the Player right and Player faces left, turn, and vice-versa
        if (input != 0 && input > 0 == FaceRight)
            playerTurn();
        

        
    }

    void FixedUpdate(){
        if (!isAlive)
            return;
        
        if (!canMove)
            input = 0;

        float targetSpeed = input * runSpeed;
        float speedDifference = targetSpeed - rb.velocity.x;
        float acceleration = (Mathf.Abs(targetSpeed) > 0.1f) ? accelerate : decelerate;
        float move = Mathf.Pow(Mathf.Abs(speedDifference) * acceleration, velocityPwr) * Mathf.Sign(speedDifference);
        rb.AddForce(move * Vector2.right * rb.mass);
    
        if (Mathf.Abs(input) < 0.01f) {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), speedDecay);

            amount *= -Mathf.Sign(rb.velocity.x);

            rb.AddForce(Vector2.right * amount, ForceMode2D.Impulse);
        }
      
    }

    private void playerTurn(){
        // NOTE: Switch player facing label
        FaceRight = !FaceRight;

        // NOTE: Multiply player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
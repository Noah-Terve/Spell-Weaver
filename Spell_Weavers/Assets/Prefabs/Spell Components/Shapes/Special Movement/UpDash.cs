using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDash : MonoBehaviour
{
    // Start is called before the first frame update   
    Vector3 PlayerVelocity;
    void Start()
    {
        //factor by which their jump speed is multiplied
        float SpeedFactor = 2f;
        
        float MinSpeed = Spell.player.GetComponent<PlayerJump>().jumpForce / 1.5f;
        float JumpForce = Spell.player.GetComponent<PlayerJump>().jumpForce;
        PlayerVelocity = Spell.player.GetComponent<PlayerJump>().rb.velocity;
        // float dir = Spell.player.GetComponent<PlayerMove>().FaceRight ? -1 : 1;
        // float difference = Spell.player.GetComponent<PlayerJump>().rb.velocity.y;
        
        //true if difference between 
        if (PlayerVelocity.y < 0) {
            // JumpForce += -PlayerVelocity.y;
            JumpForce *= 1.5f;
            PlayerVelocity.y = 0;
            // // PlayerVelocity.y = 0;
            // Spell.player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * SpeedFactor * JumpForce, ForceMode2D.Impulse);
            // return;
        }

        bool LowSpeed = (JumpForce - PlayerVelocity.y) < MinSpeed;
        
        //Player's base jump force corrected for their current move speed
        float accel = LowSpeed ? MinSpeed : (JumpForce - PlayerVelocity.y) ;
        Spell.player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * SpeedFactor * accel, ForceMode2D.Impulse);
        // Debug.Log(Spell.player.GetComponent<Rigidbody2D>().velocity);
    }
}

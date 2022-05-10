using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDash : MonoBehaviour
{
    // Start is called before the first frame update   
    void Start()
    {
        //factor by which their jump speed is multiplied
        float SpeedFactor = 2f;
        float MinSpeed = Spell.player.GetComponent<PlayerJump>().jumpForce / 2;
        float JumpForce = Spell.player.GetComponent<PlayerJump>().jumpForce;
        float CurrSpeed = Spell.player.GetComponent<PlayerJump>().rb.velocity.y;
        // float dir = Spell.player.GetComponent<PlayerMove>().FaceRight ? -1 : 1;
        // float difference = Spell.player.GetComponent<PlayerJump>().rb.velocity.y;
        
        //true if difference between 
        if (CurrSpeed < 0) {
            JumpForce += -CurrSpeed;
            // CurrSpeed = 0;
            Spell.player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * SpeedFactor * JumpForce, ForceMode2D.Impulse);
            return;
        }
        bool LowSpeed = (JumpForce - CurrSpeed) < MinSpeed;
        
        //Player's base jump force corrected for their current move speed
        float accel = LowSpeed ? MinSpeed : (JumpForce - CurrSpeed) ;
        Spell.player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * SpeedFactor * accel, ForceMode2D.Impulse);
        // Debug.Log(Spell.player.GetComponent<Rigidbody2D>().velocity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float speed = 50f;
        float dir = Spell.player.GetComponent<PlayerMove>().FaceRight ? -1 : 1;
        float accel = Spell.player.GetComponent<PlayerMove>().accelerate;
        Spell.player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed * dir * accel, ForceMode2D.Impulse);
        Debug.Log(Spell.player.GetComponent<Rigidbody2D>().velocity);
    }
}

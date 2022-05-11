using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public RuntimeVar playerHP;
    public int damage = 4;
    public float distance = 5f;
    public float speed = 5f;
    public GameObject vfx;
    public float angle = 0f;

    public float rotationSpeed = 10f;
    public GameObject rotatingArt;
    public float knockBack = 10f;

    Vector2 angleVector = Vector2.zero;

    void Start() {
        angleVector = new Vector2(-Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
        GetComponent<Rigidbody2D>().velocity = angleVector * speed;
        
        
        // Make the art of the cannon ball rotate
        rotatingArt.transform.eulerAngles = Vector3.MoveTowards(rotatingArt.transform.eulerAngles, Vector3.forward * 360, rotationSpeed * 360 * Time.deltaTime);
        if (rotatingArt.transform.eulerAngles.z >= 360)
            rotatingArt.transform.eulerAngles = Vector3.zero;

        // Destroy self after getting too far
        if (Mathf.Abs(transform.localPosition.magnitude) >= distance)
            destroySelf();
    }

    void destroySelf() {
        if (vfx != null)
            Instantiate(vfx, transform.position, vfx.transform.rotation);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D (Collider2D hit) {
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
            // pushBack -= rb.velocity.x;
        rb.AddForce(rb.mass * (Vector2.right * pushBack * accel), ForceMode2D.Impulse);

        playerHP.RuntimeVal -= damage;

        destroySelf();
    }

    
}

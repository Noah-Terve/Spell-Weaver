using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{

    public float distance = 10f, returnSpeed = 10f;

    Vector3 targetPos;

    Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.localScale.x > 0)
            distance = -distance;

        targetPos = transform.position + Vector3.right * distance;
        playerPos = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        //gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * forceX;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, returnSpeed * Time.deltaTime);
        if (transform.position == playerPos.position)
            targetPos = transform.position + Vector3.right * distance;
    }

    void OnTriggerEnter2D(Collider2D hit) {
        if (hit.gameObject.tag != "Enemy")
            return;
        targetPos = playerPos.position;
    }
}

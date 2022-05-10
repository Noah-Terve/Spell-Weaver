using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBoomerang : MonoBehaviour
{
    // Start is called before the first frame update
    public float distance = 10f, returnSpeed = 10f;

    Vector3 targetPos;

    Vector3 iniPos;
    Transform playerPos;
    Vector3 iniPlayerPos;
    void Start()
    {
        StartCoroutine(WaitDestroy());
        if (transform.localScale.x > 0)
            distance = -distance;
        iniPos = transform.position;
         
        targetPos = transform.position + Vector3.right * distance;
        playerPos = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        iniPlayerPos = playerPos.position;
        // gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(distance, 0);
        //gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * forceX;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, iniPlayerPos, returnSpeed * Time.deltaTime / 1.5f);
        // if (distance < 0) {
        //     if (transform.position < distance)
        // }
        if (transform.position == playerPos.position)
            targetPos = transform.position + Vector3.right * distance;
    }

    void OnCollisionEnter2D(Collision2D hit) {
        if (hit.gameObject.tag != "Player")
            return;
        targetPos = iniPos;
    }

    IEnumerator WaitDestroy() {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

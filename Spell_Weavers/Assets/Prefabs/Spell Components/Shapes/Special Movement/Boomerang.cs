using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{

    public float forceX = 0f, returnPwr = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.localScale.x > 0)
            forceX = -forceX;
        
        if (transform.localScale.x < 0)
            returnPwr = -returnPwr;

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * forceX;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity + Vector2.right * returnPwr;
    }
}

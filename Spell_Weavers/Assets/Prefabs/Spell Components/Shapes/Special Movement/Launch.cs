using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public float forceX = 0f, forceY = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (transform.localScale.x > 0)
            forceX = -forceX;

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(forceX, forceY);
    }

}

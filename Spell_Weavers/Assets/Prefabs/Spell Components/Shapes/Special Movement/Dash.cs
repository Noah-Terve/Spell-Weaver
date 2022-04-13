using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    
    GameObject follow;
    // Start is called before the first frame update
    void Start()
    {
        follow = Spell.player;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.transform.position;
    }
}

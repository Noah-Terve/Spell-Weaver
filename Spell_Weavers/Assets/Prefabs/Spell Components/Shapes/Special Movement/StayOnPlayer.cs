using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(Spell.player.transform);   
    }
}

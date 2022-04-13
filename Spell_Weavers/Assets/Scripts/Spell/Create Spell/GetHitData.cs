using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *    Name: GetHitData.cs
 * Purpose: Finds enemies and calculates upon triggering
 *    Date: Created 3/17/2022 by Matthew
 *    Note: 
 */
public class GetHitData : MonoBehaviour
{
    public Spell spell;

    public float knockBack = 1;

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == spell.targetTag && spell != null)
        {
            spell.activateEffects(hit.gameObject);
            spell.dmgCalc(hit.gameObject);


            Vector2 direction = hit.gameObject.transform.position - gameObject.transform.position;
            direction.Normalize() ;
            // TESTING PURPOSES
            hit.gameObject.GetComponent<Rigidbody2D>().velocity += (direction)* knockBack;
        }
    }
}

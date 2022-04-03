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

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Enemy" && spell != null)
        {
            spell.activateEffects(hit.gameObject);
            spell.dmgCalc(hit.gameObject);
        }
    }
}

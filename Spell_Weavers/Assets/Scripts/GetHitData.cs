using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *    Name: ShapeComponent
 * Purpose: An inheritable class which represents all spell components (shape, element)
 *    Date: Created 3/17/2022 by Matthew
 */
public class GetHitData : MonoBehaviour
{
    public Spell spell;

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Enemy" && spell != null)
        {
            spell.dmgCalc(hit.gameObject);
        }
    }
}

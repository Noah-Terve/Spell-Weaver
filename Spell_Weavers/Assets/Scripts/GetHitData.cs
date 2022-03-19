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

    HashSet<GameObject> alreadyHit = new HashSet<GameObject>();

    void OnTriggerStay2D(Collider2D hit)
    {
        Debug.Log("ENTER");

        if (hit.gameObject.tag == "Enemy" && spell != null && !alreadyHit.Contains(hit.gameObject))
        {
            alreadyHit.Add(hit.gameObject);
            spell.dmgCalc(hit.gameObject);
        }
            
    }

    void OnTriggerExit2D(Collider2D hit)
    {
        alreadyHit.Remove(hit.gameObject);
    }
}

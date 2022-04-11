using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *    Name: EffectsList.cs
 * Purpose: The list of all possible special effects
 *    Date: Created 3/22/2022 by Matthew
 */
public class EffectsList : MonoBehaviour
{
/* TEMPLATE */
/* 
    public static void NAME (GameObject player, GameObject target, Spell spell) {
        // Whatever the effect is 
    }
*/

/* LIST OF EFFECTS */
    public static void move (GameObject player, GameObject target, Spell spell) {
        target.transform.position = (target.transform.position + player.transform.position) / 2;
    }
    public static void burn (GameObject player, GameObject target, Spell spell) {
        // Whatever the effect is 
    }
    // TODO:: TALK ABOUT THE HP
    public static void lifeSteal (GameObject player, GameObject target, Spell spell) {
        player.GetComponent<PlayerHealth>().Health.InitialVal += spell.dmg;
    }
}

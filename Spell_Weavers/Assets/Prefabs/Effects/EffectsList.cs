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
    public static void NAME (GameObject player, GameObject target) {
        // Whatever the effect is 
    }
*/

/* LIST OF EFFECTS */
    public static void move (GameObject player, GameObject target) {
        target.transform.position = (target.transform.position + player.transform.position) / 2;
    }
    public static void burn (GameObject player, GameObject target) {
        // Whatever the effect is 
    }
}

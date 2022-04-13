using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *    Name: StayOnPlayer
 * Purpose: Makes it stick to the player (for spells)
 *    Date: Created 3/17/2022 by Matthew
 */
public class StayOnPlayer : MonoBehaviour
{
    /*
     *       Name: Start()
     * Parameters: None
     *     Return: None
     *    Purpose: Makes the gameobject the child of the parent
     *       Note: Runs before the first frame
     */
    void Start()
    {
        transform.SetParent(Spell.player.transform);   
    }
}

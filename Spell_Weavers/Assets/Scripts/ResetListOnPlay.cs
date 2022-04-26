using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *    Name: ResetListOnPlay
 * Purpose: Clears the spell list on exiting play
 *    Date: Created 4/26/2022 by Matthew
 */
public class ResetListOnPlay : MonoBehaviour
{
    public SpellComponentList list;
    
    /*
     *       Name: OnApplicationQuit()
     * Parameters: None
     *     Return: None
     *    Purpose: When the application is quit, then erase all of the components
     *       Note: 
     */
    void OnApplicationQuit() {
        list.allElements = new ElementComponent[0];
        list.allShapes = new ShapeComponent[0];
        list.allEffects = new EffectComponent[0];
    }
}

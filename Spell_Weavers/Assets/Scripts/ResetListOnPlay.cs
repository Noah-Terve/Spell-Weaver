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
    
    static ElementComponent[] elems;
    static ShapeComponent[] shape;
    static EffectComponent[] eff;

    public void updateLearned() {
        // System.Array.Copy(list.allElements, elems, list.allElements.Length);
        // System.Array.Copy(list.allShapes, shape, list.allShapes.Length);
        // System.Array.Copy(list.allEffects, eff, list.allEffects.Length);
        // if (list.allElements.Length != 0) {
        //     List<ElementComponent> el = new List<ElementComponent>(list.allElements);
        //     elems = el.ToArray();
        // }

        // if (list.allShapes.Length != 0) {
        //     List<ShapeComponent>   s  = new List<ShapeComponent>(list.allShapes);
        //     shape = s.ToArray();
        // }

        // if (list.allEffects.Length != 0) {
        //     List<EffectComponent>  ef = new List<EffectComponent>(list.allEffects);
        //     eff   = ef.ToArray();
        // }
        elems = list.allElements;
        shape = list.allShapes;
        eff   = list.allEffects;
        
        
        
    }

    public void setSpellList() {
        list.allElements = elems;
        list.allShapes = shape;
        list.allEffects = eff;

        
    }

    void Awake() {
        setSpellList();
    }

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

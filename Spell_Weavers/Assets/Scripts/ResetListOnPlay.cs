using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetListOnPlay : MonoBehaviour
{
    public SpellComponentList list;
    
    void OnApplicationQuit() {
        list.allElements = new ElementComponent[0];
        list.allShapes = new ShapeComponent[0];
        list.allEffects = new EffectComponent[0];
    }
}

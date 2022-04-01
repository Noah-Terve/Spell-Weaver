using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *    Name: SpellComponentList.cs
 * Purpose: Makes a single Scriptable Object that contains all of the types of components
 *    Date: Created 3/17/2022 by Matthew
 */ 

[CreateAssetMenu(fileName = "New List of All Spells", menuName = "All Spell List")]
public class SpellComponentList : ScriptableObject
{
    public ElementComponent[] allElements;
    public ShapeComponent[] allShapes;
    public EffectComponent[] allEffects;
}

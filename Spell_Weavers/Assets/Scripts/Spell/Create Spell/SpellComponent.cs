using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *    Name: ShapeComponent
 * Purpose: An inheritable class which represents all spell components (shape, element)
 *    Date: Created 3/17/2022 by Matthew
 */
public abstract class SpellComponent : ScriptableObject
{
    public float cooldown = 10f;
    public float castTime = 0.4f;
    public float dmg = 2f;
    public float lingering = 0.2f;
    [TextArea(5,10)]
    public string description;
}

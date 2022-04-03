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
    public float cooldown = 1f;
    public float castTime = 1f;
    public float dmg = 1f;
}

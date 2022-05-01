using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *    Name: ElementComponent.cs
 * Purpose: Respresents the element component
 *    Date: Created 3/17/2022 by Matthew
 */

[CreateAssetMenu(fileName = "New Element", menuName = "Spell Element")]
public class ElementComponent : SpellComponent
{
    public GameObject vfx;
    public float sizeMultiplier = 1f;
    public Element strongAgainst;
}

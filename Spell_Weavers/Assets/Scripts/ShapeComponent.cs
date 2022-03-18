using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *    Name: ShapeComponent.cs
 * Purpose: Respresents the shape component
 *    Date: Created 3/17/2022 by Matthew
 */

[CreateAssetMenu(fileName = "New Shape", menuName = "Spell Shape")]
public class ShapeComponent : SpellComponent
{
    public float xMove = 0f, yMove = 0f;
    public GameObject shapePrefab;
}

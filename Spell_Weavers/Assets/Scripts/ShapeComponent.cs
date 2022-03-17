using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shape", menuName = "Spell Shape")]
public class ShapeComponent : SpellComponent
{
    public float xDisplace = 0f, yDisplace = 0f;
    public GameObject shapePrefab;
}

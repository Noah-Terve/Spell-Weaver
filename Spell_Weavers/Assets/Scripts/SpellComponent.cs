using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SpellComponent : ScriptableObject
{
    float cooldown = 1f;
    float speed = 1f;
    float dmg = 1f;
}

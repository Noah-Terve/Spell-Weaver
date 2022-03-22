using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 *    Name: EffectComponent.cs
 * Purpose: Respresents Extra effects that can also happen
 *    Date: Created 3/17/2022 by Matthew
 */

[CreateAssetMenu(fileName = "New Effect", menuName = "Spell Effect")]
public class EffectComponent : SpellComponent
{
    public UnityEvent<GameObject, GameObject> effect;
    
    public void triggerEffect(GameObject player, GameObject target) {
        effect.Invoke(player, target);
    }
}

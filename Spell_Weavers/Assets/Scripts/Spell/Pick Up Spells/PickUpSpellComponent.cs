using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpellComponent : MonoBehaviour
{
    // The spell components that get picked up 
    public SpellComponent[] pickUp;
    public SpellComponentList knownList;

    HoldComponents hold;

    void Start() {
        hold = GameObject.FindWithTag("Spell Menu").GetComponent<HoldComponents>();
    }
    
    public void addComponents() {
        List<ElementComponent> element = new List<ElementComponent>(knownList.allElements);
        List<ShapeComponent> shape = new List<ShapeComponent>(knownList.allShapes);
        List<EffectComponent> effect = new List<EffectComponent>(knownList.allEffects);

        foreach (SpellComponent s in pickUp) {
            if (s is ElementComponent && !element.Contains(s as ElementComponent))
                element.Add(s as ElementComponent);
            else if (s is ShapeComponent  && !shape.Contains(s as ShapeComponent))
                shape.Add(s as ShapeComponent);
            else if (s is EffectComponent  && !effect.Contains(s as EffectComponent))
                effect.Add(s as EffectComponent);
        }

        knownList.allElements = element.ToArray();
        knownList.allShapes = shape.ToArray();
        knownList.allEffects = effect.ToArray();

        hold.updateMenu();
    }

    public void destroyObject() {
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D() {
        addComponents();
        destroyObject();
    }
}

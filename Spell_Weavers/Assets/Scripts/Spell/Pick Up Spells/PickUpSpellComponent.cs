using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *    Name: PickUpSpellComponent
 * Purpose: Allows for a component to be picked up
 *    Date: Created 4/20/2022 by Matthew
 */
public class PickUpSpellComponent : MonoBehaviour
{
    // The spell components that get picked up 
    public SpellComponent[] pickUp;
    public SpellComponentList knownList;

    HoldComponents hold;


    /*
     *       Name: Awake()
     * Parameters: None
     *     Return: None
     *    Purpose: Finds the spell menu
     *       Note: 
     */
    void Awake() {
        hold = GameObject.FindWithTag("Spell Menu").GetComponent<HoldComponents>();
    }

    /*
     *       Name: addComponents()
     * Parameters: None
     *     Return: None
     *    Purpose: Adds the components in the component list pick up to the component list
     *       Note: Called in OnTriggerEnter2D
     */
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

    /*
     *       Name: OnTriggerEnter2D()
     * Parameters: addComponents();
     *     Return: None
     *    Purpose: Adds the component, and destroys self
     *       Note: 
     */
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            addComponents();
            Destroy(gameObject);
        }
    }
}

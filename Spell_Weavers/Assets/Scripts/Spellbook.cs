using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *    Name: Spellbook.cs
 * Purpose: Respresents the storage of all of the spells to be attached to a child of the player gameobject; keeps track of all the spells
 *    Date: Created 3/18/2022 by Matthew
 */ 
public class Spellbook : MonoBehaviour
{
    // Adding references to the spell Components
    public SpellComponent[] allElements;
    public SpellComponent[] allShapes;

    public List<Spell> spells = new List<Spell>();

    // Make sure not to make a new spell when already have one there
    static HashSet<Spell> knownSpells = new HashSet<Spell>();
    // Check whether they are on cooldown
    HashSet<Spell> onCooldown = new HashSet<Spell>();

    // Start is called before the first frame update
    void Start()
    {
        Spell.player = GameObject.FindGameObjectsWithTag("Player")[0];
        // TESTING
        SpellComponent[] test = { allElements[0], allShapes[0] };
        addSpell(test);
        SpellComponent[] test2 = { allElements[1], allShapes[0] };
        addSpell(test2);

    }
    HashSet<Spell> removeSet = new HashSet<Spell>();
    // Update is called once per frame
    void Update()
    {
        
        foreach (Spell s in onCooldown)
        {
            s.decrementCooldown();
            if (!s.isOnCooldown())
                removeSet.Add(s);
        }
        onCooldown.ExceptWith(removeSet);
        removeSet.Clear();

        // TESTING
        if (Input.GetButtonDown("Fire1") && !spells[1].isOnCooldown()) {
            startSpellCast(spells[1]);
            onCooldown.Add(spells[1]);
            Debug.Log("FIRE");
        }
        
    }

    public void addSpell(SpellComponent[] components)
    {
        if (components == null)
            return;

        Spell s = new Spell(components);

        if (!knownSpells.Contains(s)) {
            knownSpells.Add(s);
            // test
            Debug.Log("Get Here " + s.ToString());
            spells.Add(s);
        }
    }

    public void resetSpellBook()
    {
        onCooldown.Clear();
        knownSpells.Clear();
    }

    /*
     *       Name: startSpellCast()
     * Parameters: None
     *     Return: None
     *    Purpose: Activates all the parts
     *       Note: Starts a coroutine, and the damage calculation is at the level of the game object hitboxes
     */
    public void startSpellCast(Spell s)
    {
        if (s == null) {
            Debug.Log("No Spell");
            return;
        }
        s.startSpellTimers();
        StartCoroutine(s.castSpell());
    }
}

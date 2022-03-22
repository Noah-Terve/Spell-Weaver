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
/* IV */
    // Adding references to the spell Components 
    // TODO:: Figure out how to remove this part - Shouldn't have to make this array every time (May not be able to remove, but in the prefab)
    public ElementComponent[] allElements;
    public ShapeComponent[] allShapes;
    public EffectComponent[] allEffects;

    // Spells to be cast
    public static List<Spell> spells = new List<Spell>();

    // Make sure not to make a new spell when already have one there
    static HashSet<Spell> knownSpells = new HashSet<Spell>();
    // Check whether they are on cooldown
    static HashSet<Spell> onCooldown = new HashSet<Spell>();


/* METHODS */
    /*
     *       Name: Start()
     * Parameters: None
     *     Return: None
     *    Purpose: Sets everything up, as well as finding the player GameObject
     *       Note: Start is called before the first frame update
     */
    void Start()
    {
        Spell.player = GameObject.FindGameObjectsWithTag("Player")[0];
        // TESTING
        // TODO:: REMOVE TESTING
        SpellComponent[] test = { allElements[0], allShapes[0] };
        addSpell(test);
        test = new SpellComponent[] { allElements[1], allShapes[0] };
        addSpell(test);
        test = new SpellComponent[] { allElements[1], allShapes[0] };
        addSpell(test);
        test = new SpellComponent[] { allElements[1], allShapes[0], allElements[2] };
        addSpell(test);
        test = new SpellComponent[] { allElements[2], allElements[1], allShapes[0] };
        addSpell(test);
        test = new SpellComponent[] { allElements[2], allElements[1], allShapes[0] };
        addSpell(test);
        test = new SpellComponent[] { allElements[2], allElements[1], allShapes[0] };
        addSpell(test);
        test = new SpellComponent[] { allElements[2], allElements[1], allShapes[0] };
        addSpell(test);
        test = new SpellComponent[] { allElements[2], allElements[0], allShapes[0], allEffects[0] };
        addSpell(test);

        Debug.Log(knownSpells.Count);
        foreach (Spell s in knownSpells)
            Debug.Log(s);

    }

    /*
     *       Name: Update()
     * Parameters: None
     *     Return: None
     *    Purpose: Find the input as well as control the cooldown
     *       Note: Update is called once per frame and uses a private hashSet to keep track of which spells are not on cooldown
     */
    private HashSet<Spell> removeSet = new HashSet<Spell>();
    void Update()
    {
        // Cooldown
        // TODO:: MAKE THE COOLDOWN EFFIECIENT AS WELL AS PUT INTO METHOD
        foreach (Spell s in onCooldown)
        {
            s.decrementCooldown();
            if (!s.isOnCooldown())
                removeSet.Add(s);
        }
        onCooldown.ExceptWith(removeSet);
        removeSet.Clear();

        // TESTING
        // TODO:: MAKE THE INPUT WORK
        if (Input.GetButtonDown("Fire1") && !spells[8].isOnCooldown()) {
            startSpellCast(spells[8]);
            onCooldown.Add(spells[8]);
            Debug.Log("FIRE");
        }
        
    }

    /*
     *       Name: addSpell(SpellComponent[] components)
     * Parameters: The components which make the new spell (SpellComponent[])
     *     Return: None
     *    Purpose: Adds the spell to the set of all known spells if it does not exist and adds it to the list
     *       Note: 
     */
    public void addSpell(SpellComponent[] components)
    {
        if (components == null)
            return;
        
        Spell temp = null;
        bool hasVal = false;
        
        foreach (Spell s in knownSpells) {
            if (s.getSpellComponents().SetEquals(components)) {
                hasVal = true;
                temp = s;
                break;
            }
        }

        
        if (!hasVal) {
           temp = new Spell(components);
           knownSpells.Add(temp);
        }
        spells.Add(temp);
    }

    /*
     *       Name: removeSpell(int spellIndex)
     * Parameters: Index of the spell in the list (int);
     *     Return: None
     *    Purpose: Removes the spell from the list
     *       Note: 
     */
    public void removeSpell(int spellIndex) 
    {
        spells.Remove(spells[spellIndex]);
    }
    

    /*
     *       Name: resetSpellBook()
     * Parameters: None
     *     Return: None
     *    Purpose: Clears the spellbook data
     *       Note: 
     */
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

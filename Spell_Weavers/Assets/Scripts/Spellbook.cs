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
    public SpellComponent[] allElements;
    public SpellComponent[] allShapes;

    public List<Spell> spells = new List<Spell>();

    // Make sure not to make a new spell when already have one there
    static HashSet<Spell> knownSpells = new HashSet<Spell>();
    // Check whether they are on cooldown
    HashSet<Spell> onCooldown = new HashSet<Spell>();


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
        SpellComponent[] test = { allElements[0], allShapes[0] };
        addSpell(test);
        SpellComponent[] test2 = { allElements[1], allShapes[0] };
        addSpell(test2);
        SpellComponent[] test3 = { allElements[1], allShapes[0] };
        addSpell(test3);
        
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

        
        // TODO:: THE HASHSET CAN HAVE DUPLICATES (MAY BE PROBLEM WITH EQUALS OVERLOAD)
        // HashSet<SpellComponent> temp = new HashSet<SpellComponent>(components);
        bool hasVal = false;
        foreach (Spell s in knownSpells) {
            if (s.getSpellComponents().SetEquals(components)) {
                hasVal = true;
                break;
            }
        }

        Spell temp = new Spell(components);
        if (!hasVal)
            knownSpells.Add(temp);

        spells.Add(temp);
    }

    /*
     *       Name: removeSpell()
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

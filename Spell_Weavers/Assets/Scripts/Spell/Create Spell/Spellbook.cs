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
    public SpellComponentList list;
    // Spells to be cast
    public static Queue<Spell> spells = new Queue<Spell>();

    public static bool isCasting = true;

    public static bool inCast = false;
    
    public static bool GameisPaused = false;

    // TODO:: DO WE EVEN NEED TO KEEP TRACK OF SPELLS WE HAVE SEEN?
    // Make sure not to make a new spell when already have one there
 //   static HashSet<Spell> knownSpells = new HashSet<Spell>();
    // Check whether they are on cooldown
    static HashSet<Spell> onCooldown = new HashSet<Spell>();


/* METHODS */

    void Awake()
    {
        Spell.player = GameObject.FindGameObjectsWithTag("Player")[0];
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

        if (!isCasting)
            return;

        // TESTING
        // TODO:: MAKE THE INPUT WORK
        if (Input.GetButtonDown("Fire1") && !inCast && !GameisPaused) {
            if (!castSpell())
                Debug.Log("ON COOLDOWN");
        }
        
    }

    bool castSpell() {
        if (spells.Count != 0 && !onCooldown.Contains(spells.Peek())) {
            startSpellCast(spells.Peek());
            onCooldown.Add(spells.Peek());
            Debug.Log("FIRE");
            spells.Enqueue(spells.Dequeue());

            return true;
        }
        else 
            return false;
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
        Spell s = new Spell(components);
        spells.Enqueue(s);
        Debug.Log(s);
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

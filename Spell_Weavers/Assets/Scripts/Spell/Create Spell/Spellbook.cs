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

    public static bool canCast = true;

    public static bool inCast = false;
    
    public static bool GameisPaused = false;

    // All of the cooldown information
    public static Dictionary<Spell, float> cooldownTable = new Dictionary<Spell, float>();

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
        
        if (!canCast || inCast || GameisPaused)
            return;

        // TESTING
        
        // TODO:: MAKE THE INPUT WORK
        if (Input.GetButtonDown("Fire1")) {
            if (!castSpell())
                Debug.Log("ON COOLDOWN");
        }
        
    }

    /*
     *       Name: castSpell()
     * Parameters: None
     *     Return: Returns true if the spell was cast, false if on cooldown
     *    Purpose: Cast the next spell if not on cooldown
     *       Note: 
     */
    bool castSpell() {
        if (spells.Count != 0 && !spells.Peek().isOnCooldown()) {
            startSpellCast(spells.Peek());
            addSpellCooldown(spells.Peek());
            spells.Enqueue(spells.Dequeue());
            
            Debug.Log("FIRE");

            return true;
        }
        else 
            return false;
    }

    /*
     *       Name: addSpellCooldown(Spell s)
     * Parameters: The spell to update the cooldown of (Spell)
     *     Return: None
     *    Purpose: Updates the timer table to make it contain the cooldown
     *       Note: 
     */
    void addSpellCooldown(Spell s) {
        if (cooldownTable.ContainsKey(s))
            cooldownTable[s] = s.cooldown + Time.time;
        else 
            cooldownTable.Add(s, s.cooldown + Time.time);
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
        cooldownTable.Clear();
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
        StartCoroutine(s.castSpell());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellbook : MonoBehaviour
{
    
/* IV */
    // Adding references to the spell Components 
    public SpellComponentList list;
    // Spells to be cast
    public Queue<EnemySpell> spells = new Queue<EnemySpell>();

    public bool canCast = true;

    public bool inCast = false;
    
    public bool GameisPaused = false;

    // All of the cooldown information
    public Dictionary<EnemySpell, float> cooldownTable = new Dictionary<EnemySpell, float>();

    public GameObject theEnemy;
/* METHODS */

    /*
     *       Name: Update()
     * Parameters: None
     *     Return: None
     *    Purpose: Find the input as well as control the cooldown
     *       Note: Update is called once per frame and uses a private hashSet to keep track of which spells are not on cooldown
     */

    void Update()
    {
        // WHEN IT GETS TRIGGERED
        
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
    void addSpellCooldown(EnemySpell s) {
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
        EnemySpell s = new EnemySpell(components, theEnemy, this);
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
    public void startSpellCast(EnemySpell s)
    {
        if (s == null) {
            Debug.Log("No Spell");
            return;
        }
        StartCoroutine(s.castSpell());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *    Name: Spell.cs
 * Purpose: Respresents a spell to be cast; attached to a gameobject prefab which will be cast
 *    Date: Created 3/17/2022 by Matthew
 */ 

public class Spell
{
/* IV */
    // Reference to the player shared by all spells
    public static GameObject player;

    // Given parts for the spell
    public SpellComponent[] spellParts;

    // Specs of the spell
    float castTime = 0f, cooldown = 0f, dmg = 0f; // Basic time and damage
    List<GameObject> hitboxes; // All of the prefabs to detect the damage
    HashSet<SpellComponent> spellComponents; // put into a hash set to make it so rearrangements do not matter
    HashSet<EffectComponent> effects; // All of the effects that would be triggered

    HashSet<Element> superEffective; // elemental system
    float sizeMultiplier = 1f; // The size of the spell (Water should decrease, and earth increase)
    private float cooldownTimer = 0f;

    // Spells that move you when cast
    private float xMove = 0f, yMove = 0f;

    // HASH CODE STUFF
    private int hashingCode = 0;

/* THE CONSTRUCTORS */
    /*
     *       Name: Spell()
     * Parameters: None
     *    Purpose: Default Constructor
     *       Note: 
     */
    public Spell()
    {
        spellParts = null;
    }

    /*
     *       Name: Spell(SpellCompnent[] parts)
     * Parameters: The SpellComonents that make up the spell (SpellComponent[])
     *    Purpose: Main Spell Constructor 
     *       Note: 
     */
    public Spell(SpellComponent[] parts)
    {
        spellParts = parts;
        spellSetup();
    }


/* METHODS */
    // 
    // THE CASTING
    //

    /*
     *       Name: castSpell()
     * Parameters: None
     *     Return: None
     *    Purpose: Creates all GameObjects and prevents movement for the cast time, then destroys it and lets the player move
     *       Note: Is a coroutine
     */
    public IEnumerator castSpell()
    {
        List<GameObject> theHitBoxes = new List<GameObject>();
        foreach (GameObject hb in hitboxes)
        {
            if (hb == null)
                continue;
            
            GameObject g = GameObject.Instantiate(hb, player.transform);
            g.transform.SetParent(null);
            g.transform.localScale *= sizeMultiplier;

            g.GetComponent<GetHitData>().spell = this;

            theHitBoxes.Add(g);
        }

        player.GetComponent<PlayerMove>().canMove = false;
        Spellbook.inCast = true;
        yield return new WaitForSeconds(castTime);
        
        Spellbook.inCast = false;
        player.GetComponent<PlayerMove>().canMove = true;

        foreach (GameObject hb in theHitBoxes)
            GameObject.Destroy(hb);
    }

    /*
     *       Name: dmgCalc(GameObject enemy)
     * Parameters: The enemy to damage to (GameObject)
     *     Return: None
     *    Purpose: Creates all GameObjects and prevents movement for the cast time, then destroys it and lets the player move
     *       Note: Is called in the Hitbox gameobjects when they are triggered aka onTriggerEnter
     */
    public void dmgCalc(GameObject enemy)
    {
        // TODO:: Change the HP + Knock back enemy(?)
        Debug.Log("HIT " + enemy.name + " WITH " + ToString());
    }

    /*
     *       Name: activateEffects(GameObject enemy)
     * Parameters: The enemy the spell hit (GameObject)
     *     Return: None
     *    Purpose: Triggers all of the effects
     *       Note: Is called in the Hitbox gameobjects when they are triggered aka onTriggerEnter
     */

    public void activateEffects(GameObject enemy) {
        foreach (EffectComponent e in effects)
            e.triggerEffect(player, enemy);
    }

    /*
     *       Name: getSpellComponents()
     * Parameters: None
     *     Return: The set of spell components (HashSet<SpellComponent>)
     *    Purpose: Getter for the spell components
     *       Note: 
     */
    public HashSet<SpellComponent> getSpellComponents() 
    {
        return spellComponents;
    }
    // 
    // THE SETUP
    //

    /*
     *       Name: spellSetup()
     * Parameters: None
     *     Return: None
     *    Purpose: Set up all of the parts of the spell
     *       Note: 
     */
    public void spellSetup()
    {
        spellClear();

        if (spellParts == null)
            return;


        double tempHashCode = 0f;
        // Sets all of the Spell Components up
        foreach (SpellComponent s in spellParts)
        {
            if (s == null)
                continue;

            // HashCode Setup
            tempHashCode += s.GetHashCode()/10.0;

            // All spell components affect
            cooldown += s.cooldown;
            castTime += s.castTime;
            dmg += s.dmg;

            spellComponents.Add(s);

            // Specific effects of the spell
            if (s is ElementComponent)
            {
                ElementComponent x = s as ElementComponent;
                superEffective.Add(x.strongAgainst);
                sizeMultiplier *= x.sizeMultiplier;
            }
            else if (s is ShapeComponent)
            {
                ShapeComponent x = s as ShapeComponent;
                hitboxes.Add(x.shapePrefab);
                xMove += x.xMove;
                yMove += x.yMove;
            }
            else if (s is EffectComponent)
            {
                effects.Add(s as EffectComponent);
            }

        }

        // PREVENT NEGATIVE EFFECT VALUES
        if (cooldown <= 0f)
            cooldown = 0.01f;
        if (castTime <= 0f)
            castTime = 0.01f;
        if (dmg <= 0f)
            dmg = 0.01f;
        
        while (tempHashCode > 214748364.7)
            tempHashCode -= 214748364.7;
        hashingCode = (int)(tempHashCode * 10);
    }

    /*
     *       Name: spellClear()
     * Parameters: None
     *     Return: None
     *    Purpose: Makes sure the slate is clean before starting running
     *       Note: Helper for spellSetup()
     */
    public void spellClear()
    {
        castTime = 0f;
        cooldown = 0f;
        dmg = 0f;
        xMove = 0f;
        yMove = 0f;
        sizeMultiplier = 1f;
        cooldownTimer = 0f;

        // Initialize the Sets/Lists
        hitboxes = new List<GameObject>();
        spellComponents = new HashSet<SpellComponent>();
        superEffective = new HashSet<Element>();
        effects = new HashSet<EffectComponent>();
    }

    // 
    // THE TIMERS
    //

    /*
     *       Name: isOnCooldown()
     * Parameters: None
     *     Return: true if it is on cooldown (bool)
     *    Purpose: Detect if the spell is on cooldown
     *       Note: 
     */
    public bool isOnCooldown()
    {
        return cooldownTimer > 0f;
    }

    /*
     *       Name: startSpellTimers()
     * Parameters: None
     *     Return: None
     *    Purpose: Starts the timers for the spell timers
     *       Note: 
     */
    public void startSpellTimers()
    {
        cooldownTimer = cooldown;
    }

    /*
     *       Name: decrementCooldown()
     * Parameters: None
     *     Return: None
     *    Purpose: When called on an update, it counts the cooldown
     *       Note: 
     */
    public void decrementCooldown()
    {
        if (isOnCooldown())
            cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
            cooldownTimer = 0f;
    }

    /*
     *       Name: getCooldownTimer()
     * Parameters: None
     *     Return: Returns how long until it is cooldown (float)
     *    Purpose: Get how long until the spell is usable again
     *       Note: 
     */
    public float getCooldownTimer()
    {
        return cooldownTimer;
    }

    //
    // OVERRIDES
    //

    /*
     *       Name: ToString()
     * Parameters: None
     *     Return: Returns the string representing the spell (string)
     *    Purpose: Should give the name in terms of the components
     *       Note: 
     */
    public override string ToString() {
        string str = "[ - ";
        foreach (SpellComponent s in spellComponents)
            str += s.ToString() + " - ";
        return str + "]";
    }

    /*
     *       Name: Equals(System.Object s)
     * Parameters: The object it is being compared to (System.Object)
     *     Return: Returns whether it is the same [returns false if null or not a Spell] (bool)
     *    Purpose: Changes how it sees things as equal
     *       Note: 
     */
    public override bool Equals(System.Object s) {
        if (s == null || !(s is Spell))
            return false;
        return spellComponents.SetEquals((s as Spell).spellComponents);
    }


    /*
     *       Name: GetHashCode()
     * Parameters: None
     *     Return: Returns the hashcode, which is all of the hashcodes of the spell components combined; different orderings create the same hashcode (int)
     *    Purpose: Makes different instances of the same spell go into the same spot as each other when checking the value
     *       Note: Used in the HashSet
     */
    public override int GetHashCode() {
        return hashingCode;
    }

}

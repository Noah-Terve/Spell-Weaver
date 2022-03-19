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
    HashSet<Element> superEffective; // elemental system
    float sizeMultiplier = 1f; // The size of the spell (Water should decrease, and earth increase)
    private float cooldownTimer = 0f;

    // Spells that move you when cast
    private float xMove = 0f, yMove = 0f;
    
    private bool playerCanMove = true; // TEMPORARY VARIABLE

    /* METHODS */

    //
    // THE CONSTRUCTORS
    //

    public Spell()
    {
        spellParts = null;
    }

    public Spell(SpellComponent[] parts)
    {
        spellParts = parts;
        spellSetup();
    }

    // 
    // THE CASTING
    //
    public IEnumerator castSpell()
    {
        List<GameObject> theHitBoxes = new List<GameObject>();
        foreach (GameObject hb in hitboxes)
        {
            if (hb == null)
                continue;
            GameObject g = GameObject.Instantiate(hb, player.transform);
            g.GetComponent<GetHitData>().spell = this;
            // g.transform.SetParent(player.transform);
            theHitBoxes.Add(g);
        }

        playerCanMove = false;
        yield return new WaitForSeconds(castTime);
        playerCanMove = true;

        foreach (GameObject hb in theHitBoxes)
            GameObject.Destroy(hb);
    }

    public void dmgCalc(GameObject enemy)
    {
        Debug.Log("HIT " + enemy.name + " WITH " + ToString());
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

        // Sets all of the 
        foreach (SpellComponent s in spellParts)
        {
            // All spell components affect
            cooldown += s.cooldown;
            castTime += s.castTime;
            dmg += s.dmg;

            spellComponents.Add(s);

            // Specific effects of the spell
            if (s is ElementComponent)
            {
                ElementComponent x = (ElementComponent)s;
                superEffective.Add(x.strongAgainst);
                sizeMultiplier *= x.sizeMultiplier;
            }
            else if (s is ShapeComponent)
            {
                ShapeComponent x = (ShapeComponent)s;
                hitboxes.Add(x.shapePrefab);
                xMove += x.xMove;
                yMove += x.yMove;
            }

        }

        // PREVENT NEGATIVE EFFECT VALUES
        if (cooldown <= 0f)
            cooldown = 0.01f;
        if (castTime <= 0f)
            castTime = 0.01f;
        if (dmg <= 0f)
            dmg = 0.01f;
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

    /*
     *       Name: canMove()
     * Parameters: None
     *     Return: Returns if the player can move (bool)
     *    Purpose: To detect if the player should be able to move
     *       Note: 
     */
    public bool canMove()
    {
        return playerCanMove;
    }

    //
    // OVERRIDES
    //
    // Making it so that it can check if two spells are the same or different, even if the spellComponents are rearranged
    public override bool Equals(System.Object obj)
    {
        if (obj == null)
            return false;
        Spell s = obj as Spell;
        if ((System.Object)s == null)
            return false;

        return !s.spellComponents.SetEquals(spellComponents);
    }
    public bool Equals(Spell other)
    {
        return other.spellComponents.SetEquals(spellComponents);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override string ToString() {
        string s = "";
        foreach (SpellComponent sc in spellComponents) {
            s += sc.ToString();
        }
        return s;
    }
}

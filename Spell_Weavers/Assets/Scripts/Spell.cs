using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *    Name: Spell.cs
 * Purpose: Respresents a spell to be cast; attached to a gameobject prefab which will be cast
 *    Date: Created 3/17/2022 by Matthew
 */

public class Spell : MonoBehaviour
{
/* IV */
    // Given parts for the spell
    public SpellComponent[] spellParts;

    // Specs of the spell
    float castTime = 0f, cooldown = 0f, dmg = 0f; // Basic time and damage
    List<GameObject> hitboxes; // All of the prefabs to detect the damage
    HashSet<SpellComponent> spellComponents; // put into a hash set to make it so rearrangements do not matter
    HashSet<Element> superEffective; // elemental system
    float sizeMultiplier = 1f; // The size of the spell (Water should decrease, and earth increase)
    bool onCooldown = false; // boolean dictating if it is on cooldown


    // Spells that move you when cast
    private float xMove = 0f, yMove = 0f;


/* METHODS */
    /*
     *       Name: Start()
     * Parameters: None
     *     Return: None
     *    Purpose: Set up all of the parts of the spell upon creation
     *       Note: Start is called before the first frame update
     */
    void Start()
    {
        spellSetup();
    }


    /*
     *       Name: isOnCooldown()
     * Parameters: None
     *     Return: true if it is on cooldown (bool)
     *    Purpose: Detect if the spell is on cooldown
     *       Note: 
     */
    public bool isOnCooldown()
    {
        return onCooldown;
    }

    public void startCooldown()
    {
        StartCoroutine(goOnCooldown());
    }

    IEnumerator goOnCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    /*
     *       Name: spellSetup()
     * Parameters: None
     *     Return: None
     *    Purpose: Set up all of the parts of the spell upon creation
     *       Note: Helper for Start()
     */
    public void spellSetup()
    {
        spellClear();

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

        // Initialize the Sets/Lists
        hitboxes = new List<GameObject>();
        spellComponents = new HashSet<SpellComponent>();
        superEffective = new HashSet<Element>();
    }

    // Making it so that it can check if two spells are the same or different, even if the spellComponents are rearranged
    public override bool Equals(System.Object obj)
    {
        if (obj == null)
            return false;
        Spell s = obj as Spell;
        if ((System.Object)s == null)
            return false;

        return spellComponents == s.spellComponents;
    }
    public bool Equals(Spell other)
    {
        return other.spellComponents == spellComponents;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

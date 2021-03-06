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
    public string targetTag = "Enemy";

    // Given parts for the spell
    public SpellComponent[] spellParts;

    // Specs of the spell
    public float castTime = 0f, cooldown = 0f, dmg = 0f, lingering = 0f; // Basic time and damage
    public List<GameObject> hitboxes; // All of the prefabs to detect the damage
    public HashSet<GameObject> allVfx; // The particle effects of the different types
    HashSet<SpellComponent> spellComponents; // put into a hash set to make it so rearrangements do not matter
    HashSet<EffectComponent> effects; // All of the effects that would be triggered
    


    HashSet<Element> superEffective; // elemental system
    float sizeMultiplier = 1f; // The size of the spell (Water should decrease, and earth increase)

    // Spells that move you when cast
    float xMove = 0f, yMove = 0f;

    // HASH CODE STUFF
    int hashingCode = 0;

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

        player.GetComponent<PlayerMove>().canMove = false;
        Spellbook.inCast = true;
        // player.GetComponent<Rigidbody2D>().velocity += new Vector2(xMove * player.transform.localScale.x, yMove * player.transform.localScale.y);

        float xDir = xMove * (player.GetComponent<PlayerMove>().FaceRight ? 1 : -1);
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDir, yMove), ForceMode2D.Impulse);

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
        /*
        foreach (GameObject vfx in allVfx)
            GameObject.Instantiate(vfx, player.transform.position, Quaternion.identity);
        */
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        
        yield return new WaitForSeconds(castTime);
        
        Spellbook.inCast = false;
        player.GetComponent<PlayerMove>().canMove = true;

        yield return new WaitForSeconds(lingering);
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
        delayComingAfter(enemy);
        EnemyHP enemyHp = enemy.GetComponent<EnemyHP>();
        if (enemyHp == null)
            return;

        if (superEffective.Contains(enemyHp.type))
            enemyHp.HP -= dmg * 2;
        else
            enemyHp.HP -= dmg;
        
        if (enemyHp.HP < 0)
            enemyHp.HP = 0;
        // TODO:: Change the HP + Knock back enemy(?)
        Debug.Log("HIT " + enemy.name + " WITH " + ToString());
    }

    IEnumerator delayComingAfter(GameObject enemy) {
        enemy.GetComponent<PlayerDetect>().isHit = true;
        yield return new WaitForSeconds(3f);
        enemy.GetComponent<PlayerDetect>().isHit = false;
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
            e.triggerEffect(player, enemy, this);
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
            lingering += s.lingering;

            spellComponents.Add(s);

            // Specific effects of the spell
            if (s is ElementComponent)
            {
                ElementComponent x = s as ElementComponent;
                superEffective.Add(x.strongAgainst);
                sizeMultiplier *= x.sizeMultiplier;
                
                if (x.vfx != null)
                    allVfx.Add(x.vfx);
            }
            else if (s is ShapeComponent)
            {
                ShapeComponent x = s as ShapeComponent;
                hitboxes.Add(x.shapePrefab);
                xMove -= x.xMove;
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
        if (lingering <= 0f)
            lingering = 0f;
        

        // Setting the new hash code (less efficient)
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

        // Initialize the Sets/Lists
        allVfx = new HashSet<GameObject>();
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
        return getCooldownTimer() > 0;
    }


    /*
     *       Name: getCooldownTimer()
     * Parameters: None
     *     Return: Returns how long until it is cooldown (float)
     *    Purpose: Get how long until the spell is usable again
     *       Note: Returns -1 if not in the table
     */
    public float getCooldownTimer()
    {
        if (Spellbook.cooldownTable.ContainsKey(this)) 
            if (Spellbook.cooldownTable[this] - Time.time > 0)
                return Spellbook.cooldownTable[this] - Time.time;
            else
                return 0;
        return -1;
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
        foreach (SpellComponent s in spellParts)
            str += s.name + " - ";
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

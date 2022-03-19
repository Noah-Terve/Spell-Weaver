using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour
{
    // Adding references to the spell Components
    public SpellComponent[] allElements;
    public SpellComponent[] allShapes;

    public List<Spell> spells;

    // Make sure not to make a new spell when already have one there
    static HashSet<Spell> knownSpells = new HashSet<Spell>();
    // Check whether they are on cooldown
    HashSet<Spell> onCooldown = new HashSet<Spell>();

    // Start is called before the first frame update
    void Start()
    {
        // TESTING
        SpellComponent[] test = { allElements[0], allShapes[0] };
        addSpell(test);
        SpellComponent[] test2 = { allElements[1], allShapes[0] };
        addSpell(test2);

        spells[0] = GetComponent<Spell>();

    }
    HashSet<Spell> removeSet = new HashSet<Spell>();
    // Update is called once per frame
    void Update()
    {
        
        foreach (Spell s in onCooldown)
        {
            s.decrementCooldown();
            s.decrementCastTime();
            if (!s.isOnCooldown())
                removeSet.Add(s);
        }
        onCooldown.ExceptWith(removeSet);
        removeSet.Clear();

        // TESTING
        if (Input.GetButtonDown("Fire1") && !spells[0].isOnCooldown()) {
            spells[0].startSpellCast();
            onCooldown.Add(spells[0]);
            Debug.Log("FIRE");
        }
        
    }

    public void addSpell(SpellComponent[] components)
    {
        
        gameObject.AddComponent<Spell>();
        Spell[] list = gameObject.GetComponents<Spell>();
        list[list.Length - 1].spellParts = components;
        list[list.Length - 1].spellSetup();
    }

    public void resetSpellBook()
    {
        onCooldown.Clear();
        knownSpells.Clear();
    }
}

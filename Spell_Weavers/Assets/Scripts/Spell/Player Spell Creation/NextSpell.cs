using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextSpell : MonoBehaviour
{
    
    Text text;

    void Start() {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string str = "";
        if (Spellbook.spells.Count != 0)
            str = "NEXT SPELL: " + Spellbook.spells.Peek().ToString() + "\nCOOLDOWN: " + Spellbook.spells.Peek().getCooldownTimer().ToString("F2");
        else
            str = "NEXT SPELL: None \nCOOLDOWN: None";
        text.text = str;

    }
}

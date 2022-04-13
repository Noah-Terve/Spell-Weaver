using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *    Name: UpdateSpellLisst
 * Purpose: A component which updates the spell in the slot (for the spell menu)
 *    Date: Created 3/17/2022 by Matthew
 */
public class UpdateSpellList : MonoBehaviour
{
    // Static so that it can update upon opening up the menu; this may cause bugs when switching scenes down the line
    Text textBox;
    public int slot;
    /*
     *       Name: Start()
     * Parameters: None
     *     Return: None
     *    Purpose: Finds the text box to modify
     *       Note: Runs before the first frame
     */
    void Awake()
    {
        textBox = GetComponentsInChildren<Text>()[1];
    }

    /*
     *       Name: Update()
     * Parameters: None
     *     Return: None
     *    Purpose: Makes the textbox what it is supposed to be
     *       Note: Runs every frame
     */
    void Update() {
        if (Spellbook.spells[slot] != null)
            textBox.text = Spellbook.spells[slot].ToString();
        else
            textBox.text = "None";
    }
    
}

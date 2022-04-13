using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
     *       Name: updateList()
     * Parameters: None
     *     Return: None
     *    Purpose: Updates the text to display what spells the people currently have
     *       Note: Runs whenever the spell list is updated and when the spell menu is opened
     */
    void Update() {
        if (Spellbook.spells[slot] != null)
            textBox.text = Spellbook.spells[slot].ToString();
        else
            textBox.text = "None";
    }
    
}

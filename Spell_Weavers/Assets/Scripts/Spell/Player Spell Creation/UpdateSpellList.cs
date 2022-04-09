using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSpellList : MonoBehaviour
{
    // Static so that it can update upon opening up the menu; this may cause bugs when switching scenes down the line
    static Text text;

    /*
     *       Name: Start()
     * Parameters: None
     *     Return: None
     *    Purpose: Finds the text box to modify
     *       Note: Runs before the first frame
     */
    void Awake()
    {
        text = GetComponent<Text>();
    }

    /*
     *       Name: updateList()
     * Parameters: None
     *     Return: None
     *    Purpose: Updates the text to display what spells the people currently have
     *       Note: Runs whenever the spell list is updated and when the spell menu is opened
     */
    public static void updateList() {
        Spell[] list = Spellbook.spells.ToArray();
        string str = "";

        foreach (Spell s in list) 
            str += s.ToString() + "\n";
        text.text = str;
    }
    
}

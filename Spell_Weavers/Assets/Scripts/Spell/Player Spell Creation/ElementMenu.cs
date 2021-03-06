using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class ElementMenu : MonoBehaviour
{

    static GameObject spellMenu;
    public bool canCastSpells = true;
    /*
     *       Name: Start()
     * Parameters: None
     *     Return: None
     *    Purpose: Finds the menu that can be toggled, and also sets the menu to false
     *       Note: Runs on the first frame
     */
    void Start() {
        spellMenu = GameObject.FindGameObjectsWithTag("Spell Menu")[0];
        spellMenu.SetActive(false);
        Spellbook.canCast = true;
        
        if(SceneManager.GetActiveScene().name == "Tutorial")
            canCastSpells = false;
    }
    /*
     *       Name: Update()
     * Parameters: None
     *     Return: None
     *    Purpose: Pressing E opens and closes the menu
     *       Note: Checks every frame
     */
    void Update() {
        // stop the player from accessing the spell creation menu until they
        // have unlocked it.
        if (!canCastSpells)
            return;
        
        if (Input.GetKeyDown(KeyCode.E) && !Spellbook.inCast)
            switchMenu();
        
    }

    /*
     *       Name: switchMenu()
     * Parameters: None
     *     Return: None
     *    Purpose: Toggles the menu
     *       Note: 
     */
    public static void switchMenu() {
        spellMenu.SetActive(!spellMenu.activeSelf);
        Spellbook.canCast = !spellMenu.activeSelf;
    }
}

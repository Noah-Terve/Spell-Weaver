using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldComponents : MonoBehaviour
{
    public int numComponents = 3;
    public List<SpellComponent> components = new List<SpellComponent>();

    public GameObject buttonPrefab;

    // NOTE:: SHOWS ALL SPELL COMPONENTS FOR NOW; WILL HAVE TO CHANGE LATER
    public SpellComponentList compList;

    Spellbook spellBook;
    List<GameObject> componentButtons = new List<GameObject>();

    /*
     *       Name: Start()
     * Parameters: None
     *     Return: None
     *    Purpose: Makes the menu and finds the correct spellbook
     *       Note: Starts on first frame
     */
    void Start() {
        spellBook = GameObject.FindGameObjectsWithTag("Spell Center")[0].GetComponent<Spellbook>();
        makeMenu();
    }
    /*
     *       Name: makeMenu()
     * Parameters: None
     *     Return: None
     *    Purpose: Makes all of the components button
     *       Note: 
     */
    void makeMenu() {
        for (int i = 0; i < compList.allElements.Length; i++) 
            makeButton(100, 100 + i * 30, compList.allElements[i]);
        
        for (int i = 0; i < compList.allShapes.Length; i++) 
            makeButton(220, 100 + i * 30, compList.allShapes[i]);

        for (int i = 0; i < compList.allEffects.Length; i++) 
            makeButton(340, 100 + i * 30, compList.allEffects[i]);    
    }
    /*
     *       Name: makeButton(float x, float y, SpellComponent comp)
     * Parameters: The x position of the button(float); The y position of the button (float); The component that it will add (SpellComponent)
     *     Return: None
     *    Purpose: Creates the button as a child of the gameObject
     *       Note: Is used in making the buttons
     */
    void makeButton(float x, float y, SpellComponent comp) {
        GameObject button = Instantiate(buttonPrefab, new Vector3(x, y, 0), Quaternion.identity, gameObject.transform);
        button.GetComponent<Button>().onClick.AddListener(delegate{addComponent(comp);});
        button.GetComponentInChildren<Text>().text = comp.name;
        
    }
    /*
     *       Name: addComponent(SpellComponent component)
     * Parameters: The spell component to add to the new spell (SpellComponent)
     *     Return: None
     *    Purpose: Making the spell, adding component to the list as a part of the spell
     *       Note: 
     */
    void addComponent(SpellComponent component) {
        components.Add(component);
    }
    /*
     *       Name: clearSpell()
     * Parameters: None
     *     Return: None
     *    Purpose: Clears the spell that they are currently making
     *       Note: 
     */
    void clearSpell() {
        components.Clear();
    }
    /*
     *       Name: makeSpell()
     * Parameters: None
     *     Return: None
     *    Purpose: Compiles the spell from the component list
     *       Note: Uses the components in the list
     */
    public void makeSpell() {
        spellBook.addSpell(components.ToArray());

        // TODO:: TESTING
        string s = "NEW SPELL: - ";
        foreach (SpellComponent sp in components)
            s += sp + " - ";
        Debug.Log(s);
        Debug.Log(Spellbook.spells.Peek());
        clearSpell();
        
    }

}

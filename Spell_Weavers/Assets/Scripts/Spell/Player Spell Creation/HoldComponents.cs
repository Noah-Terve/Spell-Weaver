using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldComponents : MonoBehaviour
{
    [SerializeField]
    private int initialNumComponentOnBootUp = 3;

    public static int numComponents = 3;
    public List<SpellComponent> components = new List<SpellComponent>();

    public ShapeComponent defaultAttack;
    public GameObject buttonPrefab;
    public ShowChoices choiceBox;
   //  public UpdateSpellList listDisplay;

    // NOTE:: SHOWS ALL SPELL COMPONENTS FOR NOW; WILL HAVE TO CHANGE LATER
    public SpellComponentList compList;

    Spellbook spellBook;
    List<GameObject> componentButtons = new List<GameObject>();
    bool containsShape = false;


    /*
     *       Name: Awake()
     * Parameters: None
     *     Return: None
     *    Purpose: Makes the menu and finds the correct spellbook
     *       Note: Starts before the first frame, before Start Functions
     */
    void Awake() {
        numComponents = initialNumComponentOnBootUp;
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
            makeButton(-100, 100 - i * 30, compList.allElements[i]);
        
        for (int i = 0; i < compList.allShapes.Length; i++) 
            makeButton(0, 100 - i * 30, compList.allShapes[i]);

        for (int i = 0; i < compList.allEffects.Length; i++) 
            makeButton(100, 100 - i * 30, compList.allEffects[i]);    
    }
    /*
     *       Name: makeButton(float x, float y, SpellComponent comp)
     * Parameters: The x position of the button(float); The y position of the button (float); The component that it will add (SpellComponent)
     *     Return: None
     *    Purpose: Creates the button as a child of the gameObject
     *       Note: Is used in making the buttons
     */
    void makeButton(float x, float y, SpellComponent comp) {
        GameObject button = Instantiate(buttonPrefab, gameObject.transform);
        button.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
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
        if (components.Count >= numComponents)
            return;

        components.Add(component);
        if (component is ShapeComponent)
            containsShape = true;
        
        choiceBox.updateText();
    }
    
    /*
     *       Name: clearSpell()
     * Parameters: None
     *     Return: None
     *    Purpose: Clears the spell that they are currently making
     *       Note: 
     */
    public void clearSpell() {
        components.Clear();
        choiceBox.updateText();
        containsShape = false;
    }
    /*
     *       Name: makeSpell()
     * Parameters: None
     *     Return: None
     *    Purpose: Compiles the spell from the component list
     *       Note: Uses the components in the list
     */
    public void makeSpell() {
        if (components.Count == 0)
            return;
        
        if (!containsShape)
            components.Add(defaultAttack);
        
        spellBook.addSpell(components.ToArray());
        UpdateSpellList.updateList();
        
        // TODO:: TESTING
        string s = "NEW SPELL: - ";
        foreach (SpellComponent sp in components)
            s += sp + " - ";
        Debug.Log(s);
        Debug.Log(Spellbook.spells.Peek());
        // END OF TESTING

        clearSpell();
        
    }
    /*
     *       Name: clearSpellList()
     * Parameters: None
     *     Return: None
     *    Purpose: Clears the spell list that they currently have
     *       Note: 
     */
    public void clearSpellList() {
        Spellbook.spells.Clear();
        UpdateSpellList.updateList();
    }
}

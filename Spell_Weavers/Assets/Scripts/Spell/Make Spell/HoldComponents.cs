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


    void Start() {
        
        spellBook = GameObject.FindGameObjectsWithTag("Spell Center")[0].GetComponent<Spellbook>();
        makeMenu();
    }

    void makeMenu() {
        for (int i = 0; i < compList.allElements.Length; i++) 
            makeButton(100, 100 + i * 30, compList.allElements[i]);
        
        for (int i = 0; i < compList.allShapes.Length; i++) 
            makeButton(220, 100 + i * 30, compList.allShapes[i]);

        for (int i = 0; i < compList.allEffects.Length; i++) 
            makeButton(340, 100 + i * 30, compList.allEffects[i]);    
    }

    void makeButton(float x, float y, SpellComponent comp) {
        GameObject button = Instantiate(buttonPrefab, new Vector3(x, y, 0), Quaternion.identity, gameObject.transform);
        button.GetComponent<Button>().onClick.AddListener(delegate{addComponent(comp);});
        button.GetComponentInChildren<Text>().text = comp.name;
        
    }

    void addComponent(SpellComponent component) {
        components.Add(component);
    }

    void clearSpell() {
        components.Clear();
    }

    public void makeSpell() {
        spellBook.addSpell(components.ToArray());
        
        string s = "NEW SPELL: - ";
        foreach (SpellComponent sp in components)
            s += sp + " - ";
        Debug.Log(s);
        Debug.Log(Spellbook.spells[Spellbook.spells.Count - 1]);
        clearSpell();
        
    }

}

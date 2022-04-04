using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowChoices : MonoBehaviour
{
    Text text; 
    
    public HoldComponents compHolder;

    /*
     *       Name: Start()
     * Parameters: None
     *     Return: None
     *    Purpose: Finds the text box to modify
     *       Note: Runs before the first frame
     */
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        updateText();
    }

    /*
     *       Name: Start()
     * Parameters: None
     *     Return: None
     *    Purpose: Updates the choices
     *       Note: Depends on how many components are allowed
     */
    public void updateText() {
        string str = "";
        foreach (SpellComponent s in compHolder.components)
            str += s.name + " ";

        for (int i = 0; i < HoldComponents.numComponents - compHolder.components.Count; i++) 
            str += "_____ ";

        text.text = str;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *    Name: SpellComponentContainer
 * Purpose: Stores what Component is actually in the button
 *    Date: Created 3/17/2022 by Matthew
 */
public class SpellComponentContainer : MonoBehaviour
{
    public static Text text;

    public SpellComponent comp;

    public void writeDescription() {
        text.text = comp.name + "\n---\n" + comp.description;
    }
    
    public void eraseDescription() {
        text.text = "";
    }
}

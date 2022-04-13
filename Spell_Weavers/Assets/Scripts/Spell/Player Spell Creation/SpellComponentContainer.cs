using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

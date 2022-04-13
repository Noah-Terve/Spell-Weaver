using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextSpell : MonoBehaviour
{
    
    Text text;
    /*
     *       Name: Start()
     * Parameters: None
     *     Return: None
     *    Purpose: Find the Text of the GameObject
     *       Note: Is attached to the text stating the next spell
     */
    void Start() {
        text = GetComponent<Text>();
    }
    /*
     *       Name: Update()
     * Parameters: None
     *     Return: None
     *    Purpose: Update what the next spell will be
     *       Note: Update is called once per frame and uses a private hashSet to keep track of which spells are not on cooldown
     */
    void Update()
    {
       
    }
}

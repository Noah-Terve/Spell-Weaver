using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AllowSpells : MonoBehaviour
{
    public GameObject SpellCooldownUI;
    public static ElementMenu ElemMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        if (ElemMenu == null)
            ElemMenu = FindObjectOfType(typeof(ElementMenu)) as ElementMenu;
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player")
            SpellCooldownUI.SetActive(true);
        
        ElemMenu.canCastSpells = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows for text to appear when the player triggers certain colliders

public class UIInstructions : MonoBehaviour
{
    private IEnumerator coroutine;
    
    //TODO: Make this a list to allow for going through smaller instructions in 
    //       order
    public GameObject Instruction;

    // maybe used to play a tutorial once when a certain Gameobject is made 
    //active for the first time
    // public GameObject tutorialInstruction;
    // may be used to track when the player first opens certain menus
    //private bool openedBefore = false;
    // Start is called before the first frame update
    void Start()
    {
        Instruction.SetActive(false);
        this.enabled = false;
    }

    // Update is called once per frame, only active when text is active
    void Update()
    {
        if (Instruction.activeSelf == true) {
            if (Input.GetKeyDown(KeyCode.F)) {
                Instruction.SetActive(false);
                this.enabled = false;
                Debug.Log("Text disabled");
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D Collider){
        Debug.Log("collision occurred with the " + Collider.gameObject.tag);
        if (Collider.gameObject.tag == "Player") {
            if (Instruction.activeSelf == false) {
                Instruction.SetActive(true);
                this.enabled = true;
                Debug.Log("Text enabled");
            }
            
        }
    }
}

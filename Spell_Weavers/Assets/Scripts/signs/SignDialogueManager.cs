using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignDialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
       public Text dialogueText;
       public string[] dialogue;
       public int counter = 0;
       public int dialogueLength;

       void Start(){
              dialogueBox.SetActive(false);
              dialogueLength = dialogue.Length; //allows us test dialogue without an NPC
       }

       public void OpenDialogue(){
              dialogueBox.SetActive(true);
       }

       public void CloseDialogue(){
              dialogueBox.SetActive(false);
              dialogueText.text = "..."; //reset text
              counter = 0; //reset counter
       }

       public void LoadDialogueArray(string[] NPCscript, int scriptLength){
              dialogue = NPCscript;
              dialogueLength = scriptLength;
       }

        //function for the button to display next line of dialogue
       public void DialogueNext(){
              if (counter < dialogueLength){
                     dialogueText.text = dialogue[counter];
                     counter +=1;
              }
              else { //when lines are complete:
                     dialogueBox.SetActive(false); //turn off the dialogue display
                     dialogueText.text = "..."; //reset text
                     counter = 0; //reset counter
              }
       }
}

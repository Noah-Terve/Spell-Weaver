using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    // keep track of where the player died so they can respawn from there
    public static string SceneDied = "MainMenu";
    
    // so if i make changes here can i just auto update them to git from vs?
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
    
    // Load the beginning of the game when 
    // the botton is clicked in the main menu.
    public void StartGame() {
        SceneManager.LoadScene("Zone1", LoadSceneMode.Single);
        SceneDied = "Zone1";
    }
    
    // TODO: add an on collision enter with a checkpoint to update the SceneDied
    //       string, so we can respawn from there when we die. Not sure about
    //       how this will work, ie where exactly it needs to respan from.
    
    public void ReplayGame() {
        // TODO: add some disincentive to dying.
        
        SceneManager.LoadScene(SceneDied);
    }
}

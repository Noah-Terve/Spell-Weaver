using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    // keep track of where the player died so they can respawn from there
    public static string SceneDied = "MainMenu";
    
    
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
    }
    
    // TODO: add an on collision enter with a checkpoint to update the SceneDied
    //       string, so we can respawn from there when we die. 
    
    public void ReplayGame() {
        // TODO: add some disincentive to dying.
        
        SceneManager.LoadScene(SceneDied);
    }
}

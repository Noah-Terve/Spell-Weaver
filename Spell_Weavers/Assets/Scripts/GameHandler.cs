using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    // Start button
    public Button startButton;
    
    
    // Start is called before the first frame update
    void Start() {
        // listen for the button to be clicked so the game can start
        startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update() {
        
    }
    
    // Load the beginning of the game.
    void StartGame() {
        SceneManager.LoadScene("Zone_1", LoadSceneMode.Single);
    }
}

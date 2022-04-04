using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameHandler : MonoBehaviour
{
    // keep track of where the player died so they can respawn from there
    public static string SceneDied = "MainMenu";
    
    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;
    public AudioMixer mixer;
    public static float volumeLevel = 1.0f;
    private Slider sliderVolumeCtrl;
    

    void Awake (){
        SetLevel (volumeLevel);
        GameObject sliderTemp = GameObject.FindWithTag("PauseMenuVolumeSlider");
        if (sliderTemp != null){
            sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
            sliderVolumeCtrl.value = volumeLevel;
        }
    }
    
    // Start is called before the first frame update
    void Start() {
        pauseMenuUI.SetActive(false);
        GameisPaused = false;
        Spellbook.GameisPaused = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameisPaused) Resume();
            else Pause();
        }
    }
    
    // pause menu functions
    void Pause(){
        if (SceneDied == "MainMenu") return;
         
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        GameisPaused = true;
        Spellbook.GameisPaused = true;
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
        Spellbook.GameisPaused = false;
    }

    // Music slider in  pause menu
    public void SetLevel (float sliderValue){
        mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
        volumeLevel = sliderValue;
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
    
    // This function is for the pause menu restart, which brings them back
    // to the main menu
    public void RestartGame(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        SceneDied = "MainMenu";
    }
    
    // This function quits the entire game, again for pause menu.
    public void QuitGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}

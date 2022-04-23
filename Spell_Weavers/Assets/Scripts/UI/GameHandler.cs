using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameHandler : MonoBehaviour
{
    // keep track of where the player died so they can respawn from there
    public Transform pSpawn;
    public GameObject Player;
    
    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;
    public AudioMixer mixer;
    public static float volumeLevel = 1.0f;
    private Slider sliderVolumeCtrl;
    
    public static string [] SceneNames;
    private static int SceneIndex;
    private static int NumScenes;
    

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
        if (Player == null)
            Player = GameObject.FindWithTag("Player");
        
        if (pSpawn == null)
            pSpawn = Player.transform;
        
        // Handle scene transitions by keeping track of the index you are at.
        // usually this can be done by just setting SceneNames and then skipping
        // the for loop because we can start at 1, but if we start at
        // some random scene we need to start there.
        NumScenes = 4;
        
        if (SceneNames == null){
            SceneNames = new string[NumScenes];
            SceneNames[0] = "MainMenu";
            SceneNames[1] = "Tutorial";
            SceneNames[2] = "WaterLevel";
            SceneNames[3] = "FireLevel";
        }
        
        for (int i = 0; i < NumScenes; i++){
            if (SceneNames[i] == SceneManager.GetActiveScene().name) {
                SceneIndex = i;
                break;
            }
        }
        
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
        if (SceneManager.GetActiveScene().name == "MainMenu") return;
        
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
    
    // This function is for the pause menu restart, which brings them back
    // to the main menu
    public void RestartGame(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        SceneIndex = 0;
    }
    
    // This function quits the entire game, again for pause menu.
    public void QuitGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    
    // respawn at the last checkpoint they ran into
    public void Died(){
        Vector3 pSpn2 = new Vector3(pSpawn.position.x, pSpawn.position.y, Player.transform.position.z);
        Player.transform.position = pSpn2;
    }
    
    public void NextLevel(){
        SceneIndex++;
        SceneManager.LoadScene(SceneNames[SceneIndex], LoadSceneMode.Single);
    }
    
}

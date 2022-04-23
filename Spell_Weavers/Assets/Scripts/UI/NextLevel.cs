using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    
    public GameObject NextLevelUI;
    
    // Start is called before the first frame update
    void Start()
    {
        NextLevelUI.SetActive(false);
    }
    
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            NextLevelUI.SetActive(true);
        }
    }
    
    public void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            NextLevelUI.SetActive(false);
        }
    }
}

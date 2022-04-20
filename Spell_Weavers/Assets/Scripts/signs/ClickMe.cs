using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickMe : MonoBehaviour
{
    public GameObject ClickMeText;
    
    // Start is called before the first frame update
    void Start()
    {
        ClickMeText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player") {
            ClickMeText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag =="Player") {
            ClickMeText.SetActive(false);
        }
    }
}

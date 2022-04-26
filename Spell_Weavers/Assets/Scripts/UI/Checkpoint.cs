using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static GameHandler Handler;
    private IEnumerator coroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        // Deactivate the startpoint after a second so they can't go back to it
        if (transform.root.gameObject.name == "StartPoint"){
            coroutine = Deactivate(.5f);
            StartCoroutine(coroutine);
        }
            
        
        if (Handler == null)
            Handler = FindObjectOfType(typeof(GameHandler)) as GameHandler;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            Handler.pSpawn = gameObject.transform;
        }
    }
    
    IEnumerator Deactivate(float time){
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
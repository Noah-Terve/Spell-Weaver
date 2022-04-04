using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsText : MonoBehaviour
{
    
    private IEnumerator coroutine;
    private static GameObject Text;
    public float InstructionsAwakeFor = 15f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Text == null)
            Text = GameObject.Find("InstructionsText");
        
        if (!Text.activeInHierarchy)
            Text.SetActive(true);
        
        coroutine = Deactivate(InstructionsAwakeFor);
        StartCoroutine(coroutine);
    }
    
    // A function to be called if the user wants to see the instructions again.
    public void ReActivate(){
        if (Text.activeInHierarchy)
            return;
            
        Text.SetActive(true);
        coroutine = Deactivate(InstructionsAwakeFor);
        StartCoroutine(coroutine);
    }

    IEnumerator Deactivate(float time){
        
        yield return new WaitForSeconds(time);
        Text.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerToDelete : MonoBehaviour
{
    float time = 0.5f; 

    // Start is called before the first frame update
    void Start()
    {
        time = GetComponent<ParticleSystem>().main.duration;
        StartCoroutine(waitUntilDelete());
    }

    IEnumerator waitUntilDelete() {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}

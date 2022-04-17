using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float HP = 20;
    public Element type;

    // Update is called once per frame
    void Update()
    {
        if (HP == 0)
            gameObject.SetActive(false);
    }
}

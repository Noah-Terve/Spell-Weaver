using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float HP = 20;
    private GameObject enemy;

    void Start(){
        if (enemy == null)
            enemy = gameObject.GetComponentInParent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP == 0)
            enemy.SetActive(false);
    }
}

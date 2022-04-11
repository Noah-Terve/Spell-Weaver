using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    
    GameObject follow;
    GetHitData getHitData;
    // Start is called before the first frame update
    void Start()
    {
        getHitData = GetComponent<GetHitData>();

        if (getHitData.spell == null || getHitData.spell.targetTag != "Player")
            follow = Spell.player;
        else if (getHitData.spell is EnemySpell)
            follow = (getHitData.spell as EnemySpell).enemyInstance;
        else 
            follow = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.transform.position;
    }
}

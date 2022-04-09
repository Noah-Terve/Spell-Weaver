using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpell : Spell
{
    EnemySpellbook enemySpellbook;
    
    GameObject enemyInstance;

    public EnemySpell(SpellComponent[] parts, GameObject enemy, EnemySpellbook currSpellbook) : base(parts) {
        enemySpellbook = currSpellbook;
        enemyInstance = enemy;
        targetTag = "Player";
    }

    public override IEnumerator castSpell()
    {
        List<GameObject> theHitBoxes = new List<GameObject>();
        foreach (GameObject hb in hitboxes)
        {
            if (hb == null)
                continue;
            
            GameObject g = GameObject.Instantiate(hb, enemyInstance.transform);
            g.transform.SetParent(null);
            g.transform.localScale *= sizeMultiplier;

            g.GetComponent<GetHitData>().spell = this;

            theHitBoxes.Add(g);
        }

        // enemyInstance.GetComponent<PlayerMove>().canMove = false;
        enemySpellbook.inCast = true;
        enemyInstance.GetComponent<Rigidbody2D>().velocity += new Vector2(xMove * enemyInstance.transform.localScale.x, yMove * enemyInstance.transform.localScale.y);
        yield return new WaitForSeconds(castTime);
        
        enemySpellbook.inCast = false;
        // Enemy Move
        // enemyInstance.GetComponent<PlayerMove>().canMove = true;

        yield return new WaitForSeconds(lingering);
        foreach (GameObject hb in theHitBoxes)
            GameObject.Destroy(hb);
    }

    public override void activateEffects(GameObject pl) {
        foreach (EffectComponent e in effects)
            e.triggerEffect(enemyInstance, pl);
    }

    public override float getCooldownTimer()
    {
        if (enemySpellbook.cooldownTable.ContainsKey(this)) 
            return enemySpellbook.cooldownTable[this] - Time.time;
        return -1;
    }
}

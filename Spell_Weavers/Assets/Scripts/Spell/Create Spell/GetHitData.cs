using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *    Name: GetHitData.cs
 * Purpose: Finds enemies and calculates upon triggering
 *    Date: Created 3/17/2022 by Matthew
 *    Note: 
 */
public class GetHitData : MonoBehaviour
{
    public GameObject boomVFX;

    public Spell spell;

    public float knockBack = 1;

    /*
     *       Name: OnTriggerEnter2D(Colllider2D hit)
     * Parameters: The collider that is in the trigger (Collider2D)
     *     Return: None
     *    Purpose: When the attack hit box triggers the with the attack, it makes does the calculation on hit
     *       Note: Starts the vfx
     */
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == spell.targetTag && spell != null)
        {
            spell.activateEffects(hit.gameObject);
            spell.dmgCalc(hit.gameObject);


            Vector2 direction = hit.gameObject.transform.position - gameObject.transform.position;
            direction.Normalize();
            EnemyMoveHit enemyMove = hit.gameObject.GetComponent<EnemyMoveHit>();

            float enemySpeed = (enemyMove != null) ? enemyMove.speed : 1;

            Vector2 added = direction * knockBack * enemySpeed - hit.gameObject.GetComponent<Rigidbody2D>().velocity;

            // TESTING PURPOSES
            hit.gameObject.GetComponent<Rigidbody2D>().AddForce(added, ForceMode2D.Impulse);

            

            if (spell.allVfx.Count == 0)
            {
                GameObject boom = Instantiate(boomVFX, hit.gameObject.transform.position, boomVFX.transform.rotation);
                StartCoroutine(destroyEffects(boom));
            }
            else {
                foreach (GameObject g in spell.allVfx) {
                    GameObject vx = Instantiate(g, hit.gameObject.transform.position, g.transform.rotation);
                    vx.transform.localScale = Vector3.one * 0.5f;
                }
            }
        }
    }
    /*
     *       Name: destroyEffects(GameObject vfx)
     * Parameters: The prefab of the on hit particle effects (GameObject)
     *     Return: None
     *    Purpose: Deletes the particle effect after it is done playing 
     *       Note: Runs a coroutine
     */
    IEnumerator destroyEffects(GameObject vfx) {
        yield return new WaitForSeconds(2f);
        Destroy(vfx);
    }

    
}

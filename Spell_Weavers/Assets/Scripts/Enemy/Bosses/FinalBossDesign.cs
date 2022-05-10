using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossDesign : BossBehavior
{
    // Start is called before the first frame update

    bool UsedDM = false;

    public Animator Anim;


    public override void ExtraStart() {

        if (projectiles.Count != 4) {
            projectiles.Clear();
            projectiles.Add(GameObject.Find("FireBossAttack"));
            projectiles.Add(GameObject.Find("WaterBossAttack"));
            projectiles.Add(GameObject.Find("WindBossAttack"));
            projectiles.Add(GameObject.Find("EarthBossAttack"));
        }
        CurrPhase = Element.Water;
    }

    public override void PerformAttack(Vector3 player)
    {
        // BossHP.HP = originHP / 2;
        if (!UsedDM && BossHP.HP <= (originHP / 2)) {
            DesperationAttack(player);
            UsedDM = true;
        } else {
            BasicAttack(player, 1.25f);
        }
    }
    public override void BasicAttack(Vector3 PlayerPosition, float SizeMod) {

        Vector3 SpawnVector = transform.position;
        GameObject CurrProjectile = null;
        switch (CurrPhase)
        {
            case Element.Fire:

                CurrProjectile = projectiles[0];
                break;

            case Element.Water:
                SpawnVector.x += PlayerPosition.x > gameObject.transform.position.x ? 5 : -5;
                SpawnVector.y = PlayerPosition.y + 3;
                
                CurrProjectile = projectiles[1];
                break;


            case Element.Wind:
 
                CurrProjectile = projectiles[2];
                break;

            case Element.Earth:
                SpawnVector = PlayerPosition;
                SpawnVector.y += 5;
                CurrProjectile = projectiles[3];
                break;

            default:
                break;
        }
        GameObject attack = Instantiate (CurrProjectile, SpawnVector, Quaternion.identity);

        attack.transform.localScale *= SizeMod;
        isAttacking = false;

    }
    public override void DesperationAttack(Vector3 player)
    {
        Debug.Log("Entered DM\n");
        this.enabled = false;
        StartCoroutine(DMLoop(player, 2));
        // BasicAttack(player, 2f);
        
    }

    IEnumerator DMLoop(Vector3 Player, int itr) {
        // yield return new WaitForSeconds(1f);
        for (int j = 0; j < itr; j++) {
            for (int i = 0; i < itr; i++) {
                BasicAttack(Player, 1.25f);
                yield return new WaitForSeconds(.75f);
                Player = GameObject.FindGameObjectWithTag("Player").transform.position;
            }
            BasicAttack(Player, 2f);
        }
        isAttacking = false;
        this.enabled = true;

    }
    // Update is called once per frame

}


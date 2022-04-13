using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CooldownTimer : MonoBehaviour
{

    public int slot;

    public Image cooldownTimer;
    public GameObject noSpell;
    public Text key;

    void Start() {
        switch(slot) {
            case 0:
                key.text = "J";
            break;
            case 1:
                key.text = "K";
            break;
            case 2:
                key.text = "L";
            break;
            default:
                key.text = "X";
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Spell s = Spellbook.spells[slot];
        if (s == null)
            return;
        else
            noSpell.SetActive(false);

        if (s.isOnCooldown())
            cooldownTimer.fillAmount = s.getCooldownTimer() / s.cooldown;
        else 
            cooldownTimer.fillAmount = 0;
    }
}

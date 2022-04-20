using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float HP = 20;
    public Element type;
    private bool dying = false;
    
    private Renderer rend;
    public Animator anim;
    public GameObject healthLoot;

    void Start(){
        rend = GetComponentInChildren<Renderer> ();
        anim = GetComponentInChildren<Animator> ();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (HP <= 0 && dying == false){
            dying = true;
            Die();
        }
    }
    
    void Die(){
        // Instantiate (healthLoot, transform.position, Quaternion.identity);
        //anim.SetBool ("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(Death());
    }

    IEnumerator Death(){
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    IEnumerator ResetColor(){
        yield return new WaitForSeconds(0.5f);
        rend.material.color = Color.white;
    }
}

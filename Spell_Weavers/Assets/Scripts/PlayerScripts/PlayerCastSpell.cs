using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastSpell : MonoBehaviour
{
    public Animator animator;

    
    void Start(){
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.J))
            animator.SetTrigger("Attack1");
        if (Input.GetKeyDown(KeyCode.K))
            animator.SetTrigger("Attack1");
        if (Input.GetKeyDown(KeyCode.L))
            animator.SetTrigger("Attack2");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    Vector3 localScale;
    public float initialHP;
    private EnemyHP HP;
    public float ScaleOfHPBar = 2.0f;

	// Use this for initialization
	void Start () {
		localScale = transform.localScale;
        HP = transform.parent.gameObject.GetComponent<EnemyHP>();
        initialHP = HP.HP;
	}
	
	// Update is called once per frame
	void Update () {
		localScale.x = (HP.HP * ScaleOfHPBar) / initialHP;
		transform.localScale = localScale;
	}
}

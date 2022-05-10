using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPBar : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 localScale;
    Vector3 CurrPosition;
    public float initialHP;
    private EnemyHP HP;
    public float ScaleOfHPBar = 2.0f;

	// Use this for initialization
	void Start () {
		localScale = transform.localScale;
        CurrPosition = transform.position;
        CurrPosition.y += 5;
        HP = transform.parent.gameObject.GetComponent<EnemyHP>();
        initialHP = HP.HP;
	}
	
	// Update is called once per frame
	void Update () {
		localScale.x = (HP.HP * ScaleOfHPBar) / initialHP;
		transform.localScale = localScale;
	}
}

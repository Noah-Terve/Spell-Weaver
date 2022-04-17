using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    public RuntimeVar Health;
    Vector3 localScale;
    public float initialHP;
    public float ScaleOfHPBar = 2.0f;

	// Use this for initialization
	void Start () {
		localScale = transform.localScale;
        initialHP = Health.InitialVal;
	}
	
	// Update is called once per frame
	void Update () {
		localScale.x = (Health.RuntimeVal * ScaleOfHPBar) / initialHP;
		transform.localScale = localScale;
	}
}

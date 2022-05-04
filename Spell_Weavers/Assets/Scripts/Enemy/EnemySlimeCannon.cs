using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeCannon : MonoBehaviour
{

    public float angle = 0f;
    public float timerDelay = 1f;
    public float distance = 5f;
    public float speed = 1f;

    public GameObject cannonObject;
    public GameObject projectile;

    private Transform target;

    private Vector3 angleVector = Vector2.zero;
    private float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        cannonObject.transform.eulerAngles = Vector3.back * angle;
        angleVector = new Vector3(-Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
        if (GameObject.FindGameObjectWithTag ("Player") != null) {
            target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            if (target != null) {
                float DistToPlayer = Vector3.Distance(transform.position, target.position);
                if (DistToPlayer <= (2 * distance)) {
                    fire();
                }
            }
        }
    }

    void fire() {
        if (projectile == null)
            return;
        GameObject g = Instantiate(projectile, cannonObject.transform);
        g.transform.position += angleVector;
        CannonBall cb = g.GetComponent<CannonBall>();

        cb.angle = angle;
        cb.distance = distance;
        cb.speed = speed;
        timer = timerDelay;
    }
}

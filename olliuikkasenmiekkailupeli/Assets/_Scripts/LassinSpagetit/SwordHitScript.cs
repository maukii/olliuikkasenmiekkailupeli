using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitScript : MonoBehaviour {
    CollisionDamage cd;
	void Start () {
        cd = FindObjectOfType<CollisionDamage>();
	}
    void OnCollisionEnter(Collision col)
    {
        cd.GetCollision(col);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyWhenFinished : MonoBehaviour {
    ParticleSystem ps;
	
	void Start () {
        ps = GetComponent<ParticleSystem>();
	}
	
	void Update () {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
	}
}

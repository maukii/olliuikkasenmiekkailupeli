using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour {
    Light pl;
    public float intensity;
    public float FadeSpeed = 15;
	// Use this for initialization
	void Start () {
        pl = gameObject.GetComponent<Light>();
        intensity = 10;
        pl.intensity = intensity;
	}
	
	// Update is called once per frame
	void Update () {
        intensity = intensity - Time.deltaTime * FadeSpeed;
        pl.intensity = intensity;
        if(intensity <= 0)
        {
            Destroy(gameObject);
        }
	}
}

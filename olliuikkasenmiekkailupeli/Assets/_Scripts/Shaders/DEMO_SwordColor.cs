using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEMO_SwordColor : MonoBehaviour {

    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Sabreurs/SwordPosition");
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("up"))
        {
            Debug.Log("Inner Position");
            rend.material.SetFloat("_ColorScale", 0.0f);
        }

        if (Input.GetKey("down"))
        {
            Debug.Log("Outer Position");
            rend.material.SetFloat("_ColorScale", 1.0f);
        }
    }
}

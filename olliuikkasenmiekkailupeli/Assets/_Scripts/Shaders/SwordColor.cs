using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColor : MonoBehaviour {

    Renderer rend;
    HandAnimationControl hac;
    public bool useShaderColors = true;
    float inside = -1;
    void Start()
    {
        rend = GetComponent<Renderer>();
        hac = transform.GetComponentInParent<HandAnimationControl>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            useShaderColors = !useShaderColors;
            if (useShaderColors)
            {
                for (int i = 0; i < rend.materials.Length; i++)
                {
                    rend.materials[i].SetFloat("_ColorScale", inside);
                }
            }
            else
            {
                for (int i = 0; i < rend.materials.Length; i++)
                {
                    rend.materials[i].SetFloat("_ColorScale", 0.5f);
                }
            }
        }
        if (useShaderColors)
        {
            if (inside != hac.GetInside())
            {
                inside = hac.GetInside();
                for (int i = 0; i < rend.materials.Length; i++)
                {
                    rend.materials[i].SetFloat("_ColorScale", inside);
                }
            }
        }
        
    }
}

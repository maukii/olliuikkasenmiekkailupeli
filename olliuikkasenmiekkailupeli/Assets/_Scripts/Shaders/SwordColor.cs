using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColor : MonoBehaviour {

    Renderer rend;
    HandAnimationControl hac;
    public bool useShaderColors = true;
    float inside = -1;
    float hanging = -1;
    float swordShade = 0;
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
            if (inside != hac.GetInside() || hanging != hac.GetHanging())
            {
                inside = hac.GetInside();
                hanging = hac.GetHanging();
                if(hanging == inside)
                {
                    swordShade = 1;
                }
                else
                {
                    swordShade = 0;
                }
                for (int i = 0; i < rend.materials.Length; i++)
                {
                    rend.materials[i].SetFloat("_ColorScale", swordShade);
                }
            }
        }
        
    }
}

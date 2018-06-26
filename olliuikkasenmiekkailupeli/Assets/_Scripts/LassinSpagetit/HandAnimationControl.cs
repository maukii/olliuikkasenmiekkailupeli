using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimationControl : MonoBehaviour {
    float hanging;
    float inside;
    float strong;
    public bool Inputframe;
    public bool swordSwinging;
    public float AnimSpeed = 0.5f;
    public float InputAnimSpeed = 0.5f;
    bool vitunTriggeritL = false;
    bool vitunTriggeritR = false;

    Animator anim;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("L1") && !swordSwinging)
        {
            SwapInside();
            
        }
        if (Input.GetButtonDown("R1") && !swordSwinging)
        {

            SwapHanging();
        }
        
        if (Input.GetAxis("R2") != 0 && !swordSwinging && !vitunTriggeritR)
        {
            vitunTriggeritR = true;
            Swing();
        }
        else if(Input.GetAxis("R2") == 0 && Inputframe && swordSwinging && vitunTriggeritR)
        {
            vitunTriggeritR = false;
            Weak();
        }
        if (Inputframe && anim.GetBool("SwingDia"))
        {
            SwapInside();
            SwapHanging();
            anim.SetBool("SwingDia", false);
        }
        if (Input.GetAxis("L2") != 0 && !swordSwinging && !vitunTriggeritL)
        {
            
            vitunTriggeritL = true;
            SwingHor();
        }
        else if (Input.GetAxis("L2") == 0 && Inputframe && swordSwinging && vitunTriggeritL)
        {
            vitunTriggeritL = false;
            WeakHor();
        }
        if (Inputframe && anim.GetBool("SwingHor"))
        {
            SwapInside();
            anim.SetBool("SwingHor", false);
        }
        if (Inputframe)
        {
            anim.speed = InputAnimSpeed;
        }
        else
        {
            anim.speed = AnimSpeed;
        }
        if (Input.GetAxis("L2") == 0)
        {
            vitunTriggeritL = false;
        }
        if (Input.GetAxis("R2") == 0)
        {
            vitunTriggeritR = false;
        }
    }
    void Swing()
    {
        
        anim.SetFloat("Strong", 1);
        anim.SetBool("SwingDia", true);
        
    }
    void Weak()
    {
        anim.SetFloat("Strong", 0);
        SwapInside();
        SwapHanging();
    }
    void SwingHor()
    {

        anim.SetFloat("Strong", 1);
        anim.SetBool("SwingHor", true);

    }
    void WeakHor()
    {
        anim.SetFloat("Strong", 0);
        SwapInside();
    }
    void SwapHanging()
    {
        if(hanging == 0)
        {
            hanging = 1;
            anim.SetFloat("Hanging", 1);
        }
        else
        {
            hanging = 0;
            anim.SetFloat("Hanging", 0);
        }
    }
    void SwapInside()
    {
        if (inside == 0)
        {
            inside = 1;
            anim.SetFloat("Inside", 1);
        }
        else
        {
            inside = 0;
            anim.SetFloat("Inside", 0);
        }
    }
    void SetInside(float value)
    {
        inside = value;
        anim.SetFloat("Inside", value);
    }
    void SetHanging(float value)
    {
        hanging = value;
        anim.SetFloat("Hanging", value);
    }
    float GetInside()
    {
        inside = anim.GetFloat("Inside");
        return inside;
    }
    float GetHanging()
    {
        hanging = anim.GetFloat("Hanging");
        return inside;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimationControl : MonoBehaviour {
    public bool DEBUG_NoInput;
    public bool AdditiveStanceInput;
    public bool AdditiveInverted;
    public int AddStanceId = 1;
    float hanging;
    public float inside;
    float strong;
    public bool Inputframe;
    public bool swordSwinging;
    public float AnimSpeed = 0.5f;
    public float InputAnimSpeed = 0.5f;
    bool vitunTriggeritL = false;
    bool vitunTriggeritR = false;
    Animator anim;
    public List<string>[] mo;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        anim.SetFloat("Inside", inside);
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!DEBUG_NoInput)
        {
            if (!AdditiveStanceInput)
            {
                if (Input.GetButtonDown("L1") && !swordSwinging)
                {
                    SwapInside();

                }
                if (Input.GetButtonDown("R1") && !swordSwinging)
                {

                    SwapHanging();
                }
            }
            else
            {
                
                if (Input.GetButtonDown("L1") && !swordSwinging)
                {
                    if (AdditiveInverted)
                    {
                        AddStanceId -= 1;
                    }
                    else
                    {
                        AddStanceId += 1;
                    }

                }
                if (Input.GetButtonDown("R1") && !swordSwinging)
                {

                    if (AdditiveInverted)
                    {
                        AddStanceId += 1;
                    }
                    else
                    {
                        AddStanceId -= 1;
                    }
                }
                if(AddStanceId > 3)
                {
                    AddStanceId = 3;
                }
                else if(AddStanceId < 0)
                {
                    AddStanceId = 0;
                }
                
            }

            if (Input.GetAxis("R2") != 0 && !swordSwinging && !vitunTriggeritR)
            {
                vitunTriggeritR = true;
                Swing();
            }
            else if (Input.GetAxis("R2") == 0 && Inputframe && swordSwinging && vitunTriggeritR)
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
        if (AdditiveStanceInput)
        {
            UpdateStance(AddStanceId);
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
   public void SwapHanging()
    {
        if (!AdditiveStanceInput)
        {
            hanging = hanging == 0 ? hanging = 1 : hanging = 0;
            anim.SetFloat("Hanging", hanging);
        }
        else
        {
            if(AddStanceId == 0 || AddStanceId == 2)
            {
                AddStanceId += 1;
            }
            else
            {
                AddStanceId -= 1;
            }
        }
    }
    public void SwapInside()
    {
        if (!AdditiveStanceInput)
        {
            inside = inside == 0 ? inside = 1 : inside = 0;
            anim.SetFloat("Inside", inside);
        }
        else
        {
            if(AddStanceId == 0 || AddStanceId == 1)
            {
                AddStanceId += (3 - AddStanceId * 2);
            }
            else
            {
                AddStanceId -= (AddStanceId - 2) * 2 + 1;
            }
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
        return hanging;
    }
    void UpdateStance(int stanceId)
    {
        switch (stanceId) {
            case 0:
                SetInside(0);
                SetHanging(1);
                break;
            case 1:
                SetInside(0);
                SetHanging(0);
                break;
            case 2:
                SetInside(1);
                SetHanging(0);
                break;
            case 3:
                SetInside(1);
                SetHanging(1);
                break;
            default:
                break;
        }

    }
    
}

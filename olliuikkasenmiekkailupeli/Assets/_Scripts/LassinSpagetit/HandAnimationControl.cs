using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimationControl : MonoBehaviour {
    [Header ("--DEBUG--")]
    public bool DEBUG_NoInput;
    [Header("--Input--")]
    public bool AdditiveStanceInput;
    public bool AdditiveInverted;
    public int AddStanceId = 1;
    float hanging;
    float inside;
    
    [Header("--AnimatorSpeed--")]
    public float AnimSpeed = 1f;
    public float InputAnimSpeed = 0.8f;
    bool vitunTriggeritL = false;
    bool vitunTriggeritR = false;
    Animator anim;
    [Header("--ForAnimation--")]
    public bool Inputframe;
    public bool swordSwinging;
    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
        anim.SetFloat("Inside", inside);
        
	}
	
	// Update is called once per frame
	void Update () {

        CheckInput();
    }
    void CheckInput()
    {
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
                    AddStanceId = AdditiveInverted ? AddStanceId - 1 : AddStanceId + 1;

                }
                if (Input.GetButtonDown("R1") && !swordSwinging)
                {

                    AddStanceId = AdditiveInverted ? AddStanceId + 1 : AddStanceId - 1;
                }
                if (AddStanceId > 3)
                {
                    AddStanceId = 3;
                }
                else if (AddStanceId < 0)
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
                anim.SetFloat("SpeedMult", InputAnimSpeed);
            }
            else
            {
                anim.SetFloat("SpeedMult", AnimSpeed);
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
        
        anim.SetBool("Strong", true);
        anim.SetBool("SwingDia", true);
        
    }
    void Weak()
    {
        anim.SetBool("Strong", false);
        SwapInside();
        SwapHanging();
    }
    void SwingHor()
    {

        anim.SetBool("Strong", true);
        anim.SetBool("SwingHor", true);

    }
    void WeakHor()
    {
        anim.SetBool("Strong", false);
        SwapInside();
    }
   public void SwapHanging()
    {
        if (!AdditiveStanceInput)
        {
            hanging = hanging == 0 ? 1 : 0;
            anim.SetFloat("Hanging", hanging);
        }
        else
        {
            if (AddStanceId == 0 || AddStanceId == 2)
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
            inside = inside == 0 ? 1 : 0;
            anim.SetFloat("Inside", inside);
        }
        else
        {
            if (AddStanceId == 0 || AddStanceId == 1)
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

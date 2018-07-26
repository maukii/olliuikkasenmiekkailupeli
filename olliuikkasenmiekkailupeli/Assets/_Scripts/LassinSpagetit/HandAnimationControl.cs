using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimationControl : MonoBehaviour {
    [Header ("--DEBUG--")]
    public bool DEBUG_NoInput;
    
    [Header("--Input--")]
    public int PlayerNumber = 1;
    public bool AdditiveStanceInput;
    public bool AdditiveInverted;
    int AddStanceId = 1;
    float hanging;
    float inside;
    bool deflect = false;
    bool interrupt = false;

    [Header("--AnimatorSpeed--")]
    public bool LetThisScriptControlAnimatorSpeeds = false;
    public float AnimatorSpeed = 1f;
    public float HandAnimSpeed = 1f;
    public float InputAnimSpeed = 0.8f;


    bool[] inputDown = new bool[9];
    /* InputDown Index Table
     * 0 = A
     * 1 = B
     * 2 = X
     * 3 = Y
     * 4 = Start
     * 5 = LB
     * 6 = RB
     * 7 = LT
     * 8 = RT
     */
    Animator anim;
    [Header("--ForAnimation--")]
    public bool Inputframe;
    public bool swordSwinging;

    InputManager im;

    void Start () {
        anim = gameObject.GetComponent<Animator>();
        anim.SetFloat("Inside", inside);
        im = FindObjectOfType<InputManager>();
        if (transform.parent.name == "P2")
        {
            PlayerNumber = 2;
        }
        else
        {
            PlayerNumber = 1;
        }
    }

	void Update () {

        CheckInput();
    }

    void CheckInput()
    {
        if (!DEBUG_NoInput)
        {
            #region Input
            if (!AdditiveStanceInput)
            {
                if (im.GetLB(PlayerNumber) && !swordSwinging && !inputDown[5])
                {
                    inputDown[5] = true;
                    SwapInside();

                }
                if (im.GetRB(PlayerNumber) && !swordSwinging && !inputDown[6])
                {
                    inputDown[6] = true;
                    SwapHanging();
                }
            }
            else
            {

                if (im.GetLB(PlayerNumber) && !swordSwinging && !inputDown[5])
                {
                    inputDown[5] = true;
                    AddStanceId = AdditiveInverted ? AddStanceId - 1 : AddStanceId + 1;

                }
                if (im.GetRB(PlayerNumber) && !swordSwinging && !inputDown[6])
                {
                    inputDown[6] = true;
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

            if (im.GetRT(PlayerNumber) != 0 && !swordSwinging && !inputDown[8])
            {
                inputDown[8] = true;
                Swing();
            }
            else if (im.GetRT(PlayerNumber) == 0 && Inputframe && swordSwinging && inputDown[8])
            {
                inputDown[8] = false;
                Weak();
            }
            if (Inputframe && anim.GetBool("SwingDia"))
            {
                SwapInside();
                SwapHanging();
                anim.SetBool("SwingDia", false);
            }
            if (im.GetLT(PlayerNumber) != 0 && !swordSwinging && !inputDown[7])
            {

                inputDown[7] = true;
                SwingHor();
            }
            else if (im.GetLT(PlayerNumber) == 0 && Inputframe && swordSwinging && inputDown[7])
            {
                inputDown[7] = false;
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
                anim.SetFloat("SpeedMult", HandAnimSpeed);
            }
            if (im.GetLT(PlayerNumber) == 0)
            {
                inputDown[7] = false;
            }
            if (im.GetRT(PlayerNumber) == 0)
            {
                inputDown[8] = false;
            }
            for(int i = 0; i< inputDown.Length; i++)
            {
                if(inputDown[i] == true)
                {
                    switch (i)
                    {
                        case 0:
                            if(im.GetA(PlayerNumber) == false)
                            {
                                inputDown[i] = false;
                            }
                            break;
                        case 1:
                            if (im.GetB(PlayerNumber) == false)
                            {
                                inputDown[i] = false;
                            }
                            break;
                        case 2:
                            if (im.GetX(PlayerNumber) == false)
                            {
                                inputDown[i] = false;
                            }
                            break;
                        case 3:
                            if (im.GetY(PlayerNumber) == false)
                            {
                                inputDown[i] = false;
                            }
                            break;
                        case 4:
                            if (im.GetStart(PlayerNumber) == false)
                            {
                                inputDown[i] = false;
                            }
                            break;
                        case 5:
                            if (im.GetLB(PlayerNumber) == false)
                            {
                                inputDown[i] = false;
                            }
                            break;
                        case 6:
                            if (im.GetRB(PlayerNumber) == false)
                            {
                                inputDown[i] = false;
                            }
                            break;
                        case 7:
                            if (im.GetLT(PlayerNumber) == 0)
                            {
                                inputDown[i] = false;
                            }
                            break;
                        case 8:
                            if (im.GetRT(PlayerNumber) == 0)
                            {
                                inputDown[i] = false;
                            }
                            break;
                    }
                }
            }
            //if (Input.GetButtonDown("Xbox_P1_A"))
            //{
            //    deflect = !deflect;
            //    anim.SetBool("Deflect", deflect);
            //}
            //if (Input.GetButtonDown("Xbox_P1_B"))
            //{
            //    interrupt = !interrupt;
            //    anim.SetBool("Interrupt", interrupt);
            //}
            #endregion
        }
        if (AdditiveStanceInput)
        {
            UpdateStance(AddStanceId);
        }
        if (LetThisScriptControlAnimatorSpeeds)
        {
            anim.speed = AnimatorSpeed;
        }
    }

    #region Swings
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
    #endregion

    #region StanceSwaps
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
    #endregion

    #region SetStance
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
    public float GetInside()
    {
        inside = anim.GetFloat("Inside");
        return inside;
    }
    public float GetHanging()
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
        UpdateShader();

    }
    #endregion
    void UpdateShader()
    {
        
    }

}

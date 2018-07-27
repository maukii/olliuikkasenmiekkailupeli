using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimationControl : MonoBehaviour
{
    [Header("--DEBUG--")]
    public bool DEBUG_NoInput;

    [Header("--Input--")]
    public int PlayerNumber = 1;
    public bool AdditiveStanceInput;
    public bool AdditiveInverted;
    int AddStanceId = 1;
    float hanging;
    float inside;
    float height;
    bool deflect = false;
    bool interrupt = false;

    bool facingRight;
    public float xLimit = 5f;

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
    AnimatorStateInfo asi;
    public bool Inputframe;
    public bool swordSwinging;

    InputManager im;

    [SerializeField] int controllerLayout;

    void Start()
    {
        if(GameHandler.instance != null)
            if(GameHandler.instance.BattleStarted)
                facingRight = GetComponentInParent<AlternativeMovement5>().GetFacingRight(PlayerNumber);

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

        controllerLayout = 3; //Best Layout
        AdditiveStanceInput = true;

    }

    public int GetControllerLayout()
    {
        return controllerLayout;
    }

    void Update()
    {

        CheckInput();
        CheckControllerLayout();

        AnimationStateUpdate();

        

    }

    private void CheckControllerLayout()
    {
        if (im.GetDpad_Y(PlayerNumber) == 1)
        {
            AdditiveStanceInput = false;
            controllerLayout = 1;
        }
        else if (im.GetDpad_X(PlayerNumber) == -1)
        {
            AdditiveStanceInput = false;
            controllerLayout = 2;
        }
        else if (im.GetDpad_Y(PlayerNumber) == -1)
        {
            AdditiveStanceInput = true;
            controllerLayout = 3;
        }
        else if (im.GetDpad_X(PlayerNumber) == 1)
        {
            AdditiveStanceInput = true;
            controllerLayout = 4;
        }
    }

    void CheckInput()
    {
        if (!DEBUG_NoInput)
        {
            #region Input

            if(im.isOnlyKeyboard)
            {
                if (PlayerNumber == 1)
                {
                    if (Input.GetKeyDown(KeyCode.X))
                        SwapInside();
                    if (Input.GetKeyDown(KeyCode.C))
                        SwapHanging();

                    if (Input.GetKeyDown(KeyCode.R) && !swordSwinging)
                    {
                        Inputframe = true;
                        Swing();
                    }
                    else if (Input.GetKeyUp(KeyCode.R) && swordSwinging && Inputframe)
                    {
                        Weak();
                    }

                    if (Input.GetKeyDown(KeyCode.F) && !swordSwinging)
                    {
                        Inputframe = true;
                        SwingHor();
                    }
                    else if (Input.GetKeyUp(KeyCode.F) && swordSwinging && Inputframe)
                    {
                        WeakHor();
                    }

                    if (Input.GetKeyDown(KeyCode.H))
                    {
                        //Stab();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.P))
                        SwapInside();
                    if (Input.GetKeyDown(KeyCode.I))
                        SwapHanging();

                    if (Input.GetKeyDown(KeyCode.RightShift) && !swordSwinging)
                    {
                        Swing();
                    }
                    else if (Input.GetKeyUp(KeyCode.RightShift) && swordSwinging && Inputframe)
                    {
                        Weak();
                    }

                    if (Input.GetKeyDown(KeyCode.RightControl) && !swordSwinging)
                    {
                        SwingHor();
                    }
                    else if (Input.GetKeyUp(KeyCode.RightControl) && swordSwinging && Inputframe)
                    {
                        WeakHor();
                    }

                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        //Stab();
                    }
                }
            }
            else if(im.isKeyboardAndMouseP1 || im.isKeyboardAndMouseP2)
            {
                if((im.isKeyboardAndMouseP1 && PlayerNumber == 1) || (im.isKeyboardAndMouseP2 && PlayerNumber == 2))
                {
                    if (Input.GetMouseButtonDown(0) && !swordSwinging)
                        Swing();
                    else if (Input.GetMouseButtonUp(0) && swordSwinging && Inputframe)
                        Weak();
                    if (Input.GetMouseButtonDown(1))
                        SwapHanging();
                    if (Input.GetMouseButtonDown(2))
                        SwapInside();
                    if (Input.GetKeyDown(KeyCode.F) && !swordSwinging)
                        SwingHor();
                    else if (Input.GetKeyUp(KeyCode.F) && swordSwinging && Inputframe)
                        WeakHor();

                    if (facingRight)
                    {
                        if (Input.GetAxis("MouseX") > xLimit)
                        {
                            //Stab();
                        }
                    }
                    else if (!facingRight)
                    {
                        if (Input.GetAxis("MouseX") < -xLimit)
                        {
                            //Stab();
                        }
                    }
                }
                else
                {
                    ControllerInputs();
                }
            }                   // CONTROLS
            else if(!im.isKeyboardAndMouseP1 && !im.isKeyboardAndMouseP2)
            {
                ControllerInputs();
            }            

            if (swordSwinging && anim.GetBool("SwingDia"))
            {
                SwapInside();
                SwapHanging();
                anim.SetBool("SwingDia", false);
            }
            if (swordSwinging && anim.GetBool("SwingHor"))
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
            for (int i = 0; i < inputDown.Length; i++)
            {
                if (inputDown[i] == true)
                {
                    switch (i)
                    {
                        case 0:
                            if (im.GetA(PlayerNumber) == false)
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
                            if (im.GetTriggers(PlayerNumber) == 0)
                            {
                                inputDown[i] = false;
                            }
                            break;
                        case 8:
                            if (im.GetTriggers(PlayerNumber) == 0)
                            {
                                inputDown[i] = false;
                            }
                            break;
                        case 9:
                            if(im.GetTriggers(PlayerNumber) == 0)
                            {
                                inputDown[i] = false;
                            }
                            break;

                    }
                }
            } // controller buttons

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

    private void UpdateHandHeight(float y_input)
    {
        height = y_input;
        anim.SetFloat("Height", height);
    }

    private void ControllerInputs()
    {
        if (!AdditiveStanceInput)
        {
            if (controllerLayout == 1)
            {
                if (im.GetTriggers(PlayerNumber) > 0 && !swordSwinging && !inputDown[8])
                {
                    inputDown[8] = true;
                    SwapHanging();
                }

                if (im.GetLB(PlayerNumber) && !swordSwinging && !inputDown[5])
                {
                    inputDown[5] = true;
                    SwapInside();
                }

                if (im.GetTriggers(PlayerNumber) < 0 && !swordSwinging && !inputDown[7])
                {
                    inputDown[7] = true;
                    Inputframe = true;
                    Swing();
                }
                else if (im.GetTriggers(PlayerNumber) == 0  && Inputframe && inputDown[7])
                {
                    inputDown[7] = false;
                    Inputframe = false;
                    Weak();
                }

                if (im.GetRB(PlayerNumber) && !swordSwinging && !inputDown[6])
                {
                    inputDown[6] = true;
                    Inputframe = true;
                    SwingHor();
                }
                else if (!im.GetRB(PlayerNumber) && Inputframe  && inputDown[6])
                {
                    inputDown[6] = false;
                    Inputframe = false;
                    WeakHor();
                }

            }
            else if (controllerLayout == 2)
            {
                if (im.GetTriggers(PlayerNumber) > 0 && !swordSwinging && !inputDown[8])
                {
                    inputDown[8] = true;
                    Inputframe = true;
                    SwingHor();
                }
                else if (im.GetTriggers(PlayerNumber) == 0 && Inputframe && inputDown[8])
                {
                    inputDown[8] = false;
                    Inputframe = false;
                    WeakHor();
                }

                if (im.GetLB(PlayerNumber) && !swordSwinging && !inputDown[5])
                {
                    inputDown[5] = true;
                    SwapInside();
                }

                if (im.GetTriggers(PlayerNumber) < 0 && !swordSwinging && !inputDown[7])
                {
                    inputDown[7] = true;
                    Inputframe = true;
                    Swing();
                }
                else if (im.GetTriggers(PlayerNumber) == 0 && Inputframe && inputDown[7])
                {
                    inputDown[7] = false;
                    Inputframe = false;
                    Weak();
                }

                if (im.GetRB(PlayerNumber) && !swordSwinging && !inputDown[6])
                {
                    inputDown[6] = true;
                    SwapHanging();
                }
            }
        }
        else
        {
            if (controllerLayout == 3)
            {
                if (im.GetTriggers(PlayerNumber) > 0 && !swordSwinging && !inputDown[8])
                {
                    inputDown[8] = true;
                    Inputframe = true;
                    SwingHor();
                }
                else if (im.GetTriggers(PlayerNumber) == 0 && Inputframe && inputDown[8])
                {
                    inputDown[8] = false;
                    Inputframe = false;
                    WeakHor();
                }

                if (im.GetLB(PlayerNumber) && !swordSwinging && !inputDown[5])
                {
                    inputDown[5] = true;
                    AddStanceId = AdditiveInverted ? AddStanceId + 1 : AddStanceId - 1;
                }

                if (im.GetTriggers(PlayerNumber) < 0 && !swordSwinging && !inputDown[7])
                {
                    inputDown[7] = true;
                    Inputframe = true;
                    Swing();
                }
                else if (im.GetTriggers(PlayerNumber) == 0 && Inputframe && inputDown[7])
                {
                    inputDown[7] = false;
                    Inputframe = false;
                    Weak();
                }

                if (im.GetRB(PlayerNumber) && !swordSwinging && !inputDown[6])
                {
                    inputDown[6] = true;
                    AddStanceId = AdditiveInverted ? AddStanceId - 1 : AddStanceId + 1;
                }
            }
            else if (controllerLayout == 4)
            {
                if (im.GetTriggers(PlayerNumber) > 0 && !swordSwinging && !inputDown[8])
                {
                    inputDown[8] = true;
                    AddStanceId = AdditiveInverted ? AddStanceId - 1 : AddStanceId + 1;
                }

                if (im.GetLB(PlayerNumber) && !swordSwinging && !inputDown[5])
                {
                    inputDown[5] = true;
                    AddStanceId = AdditiveInverted ? AddStanceId + 1 : AddStanceId - 1;
                }

                if (im.GetTriggers(PlayerNumber) < 0 && !swordSwinging && !inputDown[7])
                {
                    inputDown[7] = true;
                    Inputframe = true;
                    Swing();
                }
                else if (im.GetTriggers(PlayerNumber) == 0 && Inputframe && inputDown[7])
                {
                    inputDown[7] = false;
                    Inputframe = false;
                    Weak();
                }

                if (im.GetRB(PlayerNumber) && !swordSwinging && !inputDown[6])
                {
                    inputDown[6] = true;
                    Inputframe = true;
                    SwingHor();
                }
                else if (!im.GetRB(PlayerNumber) && Inputframe && inputDown[6])
                {
                    inputDown[6] = false;
                    Inputframe = false;
                    WeakHor();
                }

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

        UpdateHandHeight(-im.GetRS_Y(PlayerNumber));
        
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
        switch (stanceId)
        {
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
    #endregion

    public float GetHeight()
    {
        return height;
    }

    void AnimationStateUpdate()
    {
        asi = anim.GetCurrentAnimatorStateInfo(1);
        if(asi.IsTag("Swing") || asi.IsTag("PullBack"))
        {
            swordSwinging = true;
        }
        else
        {
            swordSwinging = false;
        }
        if(asi.IsTag("Swing"))
        {
            Inputframe = false;
        }
    }
}

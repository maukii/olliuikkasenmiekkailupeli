using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeMovement5 : MonoBehaviour
{
    InputManager im;

    #region PlayerInfos
    [Header("----- Player Movement Axis Names -----")]
    [SerializeField] int playerIndex;
    [SerializeField] string horizontal;
    [SerializeField] string vertical;

    [SerializeField]
    float inputX, inputY, speed = 3f;

    public float xLimit = 5f;
    public float mouseX;

    [Header("--- Inputs ---")]
    [SerializeField] bool forward;
    [SerializeField] bool back;
    [SerializeField] bool attacking;
    #endregion

    public Transform p1StartPos, p2StartPos;

    [SerializeField]
    bool facingRight = false;

    [SerializeField]
    float hor, ver;

    [SerializeField]
    int controllerLayout, swordAngle;

    Animator[] anims;
    Animator anim;


    void Start()
    {
        im = FindObjectOfType<InputManager>();
        FindActiveComponents();
        SetPositionAndRotationToPlayers();
    }

    void FindActiveComponents()
    {
        anims = GetComponentsInChildren<Animator>();

        for (int i = 0; i < anims.Length; i++)
        {
            if (anims[i].enabled)
                anim = anims[i];
        }
    } // get active model's animator

    void SetPositionAndRotationToPlayers()
    {
        p1StartPos = GameObject.Find("P1_StartPosition").gameObject.transform;
        p2StartPos = GameObject.Find("P2_StartPosition").gameObject.transform;

        #region RotatePlayersRight
        if (im.isLeftP1 && playerIndex == 1)
        {
            facingRight = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (im.isLeftP2 && playerIndex == 2)
        {
            facingRight = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            facingRight = false;
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (facingRight)
        {
            transform.rotation = Quaternion.Euler(-90, 180, -90); // works on orginal models
            transform.position = p1StartPos.position;
        }
        else
        {
            transform.rotation = Quaternion.Euler(-90, -180, 90);
            transform.position = p2StartPos.position;
        }
        #endregion
    }

    void Update()
    {
        Inputs();
        Move();
    }
    
    void Inputs()
    {
        mouseX = Input.GetAxis("MouseX");
        hor = Input.GetAxis(horizontal);

        inputX = Mathf.Clamp(inputX, -1, 1);
        inputY = Mathf.Clamp(inputY, -1, 1);

        if(playerIndex == 1)
        {
            if (im.isXboxControllerP1)
                ver = -Input.GetAxis(vertical);
            else
                ver = Input.GetAxis(vertical);
        }
        else if(playerIndex == 2)
        {
            if(im.isXboxControllerP2)
                ver = -Input.GetAxis(vertical);
            else
                ver = Input.GetAxis(vertical);
            
        }
    }

    void Move()
    {

        // what layout will be used
        #region ControllerLayout
        if(playerIndex == 1)
        {
            if(im.P1_Dpad_Y == 1)
            {
                controllerLayout = 1;
            }
            else if(im.P1_Dpad_X == -1)
            {
                controllerLayout = 2;
            }
            else if(im.P1_Dpad_Y == -1)
            {
                controllerLayout = 3;
            }
            else if(im.P1_Dpad_X == 1)
            {
                controllerLayout = 4;
            }
        }
        if(playerIndex == 2)
        {
            if (im.P2_Dpad_Y == 1)
            {
                controllerLayout = 1;
            }
            else if (im.P2_Dpad_X == -1)
            {
                controllerLayout = 2;
            }
            else if (im.P2_Dpad_Y == -1)
            {
                controllerLayout = 3;
            }
            else if (im.P2_Dpad_X == 1)
            {
                controllerLayout = 4;
            }
        }
        #endregion

        // is player facing right?
        #region inputBools
        if(facingRight)
        {

            if (Input.GetAxisRaw(horizontal) == 1)
            {
                forward = true;
            }
            else
            {
                forward = false;
            }

            if (Input.GetAxisRaw(horizontal) == -1)
            {
                back = true;
            }
            else
            {
                back = false;
            }
        }
        else
        {
            if (Input.GetAxisRaw(horizontal) == 1)
            {
                back = true;
            }
            else
            {
                back = false;
            }

            if (Input.GetAxisRaw(horizontal) == -1)
            {
                forward = true;
            }
            else
            {
                forward = false;
            }
        }
        #endregion

        // what animation to play?
        #region AnimatonStuffs
        if(anim != null)
        {
            anim.SetFloat("InputX", hor);
            anim.SetBool("forward", forward);
            anim.SetBool("back", back);
        }

        if (ver >= 0.1f && inputY < 1f)
        {
            inputY += speed * Time.deltaTime;
            if(anim != null)
                anim.SetFloat("InputY", inputY);
        }
        if (ver <= -0.1f && inputY > -1f)
        {
            inputY -= speed * Time.deltaTime;
            if(anim != null)
                anim.SetFloat("InputY", inputY);
        }

        if (ver == 0)
        {
            inputY = Mathf.Lerp(inputY, 0, speed * Time.deltaTime);
            if ((inputY <= 0.02f && inputY > 0) || (inputY >= -0.02f && inputY < 0))
            {
                inputY = 0f;
            }
            if(anim != null)
                anim.SetFloat("InputY", inputY);
        }
        #endregion

        // what attack to play?
        #region AttacksWhenInput
        if(playerIndex == 1)
        {
            if(!attacking)
            {
                if(im.isXboxControllerP1 || im.isPSControllerP1)
                {
                    if(controllerLayout == 1) // TODO: CHANGE TO CORRECT INPUTS
                    {
                        if(im.P1_LT > 0)
                            ChangeGuard();
                        if(im.P1_LB)
                            ChangeSide();
                        if (im.P1_RT > 0)
                            VerticalSlash();
                        if (im.P1_RB)
                            HorizontalSlash();
                    }
                    else if (controllerLayout == 2)
                    {
                        if (im.P1_LT > 0)
                            HorizontalSlash();
                        if (im.P1_LB)
                            ChangeSide();
                        if (im.P1_RT > 0)
                            VerticalSlash();
                        if (im.P1_RB)
                            ChangeGuard();
                    }
                    else if (controllerLayout == 3)
                    {
                        if (im.P1_LT > 0)
                            HorizontalSlash();
                        if (im.P1_LB)
                            TurnSword(facingRight?-1:1);
                        if (im.P1_RT > 0)
                            VerticalSlash();
                        if (im.P1_RB)
                            TurnSword(facingRight?1:-1);
                    }
                    else if (controllerLayout == 4)
                    {
                        if (im.P1_LT > 0)
                            TurnSword(facingRight?-1:1);
                        if (im.P1_LB)
                            ChangeSide();
                        if (im.P1_RT > 0)
                            VerticalSlash();
                        if (im.P1_RB)
                            HorizontalSlash();
                    }
                }
                else if(im.isKeyboardAndMouseP1)
                {
                    if (Input.GetMouseButtonDown(0)) // left
                        VerticalSlash();
                    if (Input.GetMouseButtonDown(1)) // right
                        ChangeGuard();
                    if (Input.GetMouseButtonDown(2)) // middle
                        ChangeSide();
                    if (Input.GetKeyDown(KeyCode.F))
                        HorizontalSlash();

                    if(facingRight)
                    {
                        if (Input.GetAxis("MouseX") > xLimit)
                            Stab();
                    }
                    else if(!facingRight)
                    {
                        if(Input.GetAxis("MouseX") < -xLimit)
                        {
                            Stab();
                        }
                    }
                }

                else if(im.isOnlyKeyboard)
                {
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        ChangeSide();
                    }
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        ChangeGuard();
                    }
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        VerticalSlash();
                    }
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        HorizontalSlash();
                    }
                    if (Input.GetKeyDown(KeyCode.H))
                    {
                        Stab();
                    }
                }
            }
        }
        else if(playerIndex == 2)
        {
            if (!attacking)
            {
                if (im.isXboxControllerP2 || im.isPSControllerP2)
                {
                    if(controllerLayout == 1)
                    {
                        if (im.P2_LT > 0)
                            ChangeGuard();
                        if (im.P2_LB)
                            ChangeSide();
                        if (im.P2_RT > 0)
                            VerticalSlash();
                        if (im.P2_RB)
                            HorizontalSlash();
                    }
                    else if(controllerLayout == 2)
                    {
                        if (im.P2_LT > 0)
                            ChangeGuard();
                        if (im.P2_LB)
                            ChangeSide();
                        if (im.P2_RT > 0)
                            VerticalSlash();
                        if (im.P2_RB)
                            HorizontalSlash();
                    }
                    else if(controllerLayout == 3)
                    {
                        if (im.P2_LT > 0)
                            HorizontalSlash();
                        if (im.P2_LB)
                            TurnSword(facingRight ? -1 : 1);
                        if (im.P2_RT > 0)
                            VerticalSlash();
                        if (im.P2_RB)
                            TurnSword(facingRight ? 1 : -1);
                    }
                    else if(controllerLayout == 4)
                    {
                        if (im.P2_LT > 0)
                            TurnSword(facingRight ? 1 : -1);
                        if (im.P2_LB)
                            ChangeSide();       
                        if (im.P2_RT > 0)
                            VerticalSlash();
                        if (im.P2_RB)
                            HorizontalSlash();
                    }
                }

                else if (im.isKeyboardAndMouseP2)
                {
                    if (Input.GetMouseButtonDown(0)) // left
                        VerticalSlash();
                    if (Input.GetMouseButtonDown(1)) // right
                        ChangeGuard();
                    if (Input.GetMouseButtonDown(2)) // middle
                        ChangeSide();
                    if (Input.GetKeyDown(KeyCode.F))
                        HorizontalSlash();

                    if (facingRight)
                    {
                        if (Input.GetAxis("MouseX") > xLimit)
                            Stab();
                    }
                    else if(!facingRight)
                    {
                        if (Input.GetAxis("MouseX") < -xLimit)
                        {
                            Stab();
                        }
                    }
                }

                else if(im.isOnlyKeyboard)
                {
                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        ChangeSide();
                    }
                    if (Input.GetKeyDown(KeyCode.I))
                    {
                        ChangeGuard();
                    }
                    if (Input.GetKeyDown(KeyCode.RightShift))
                    {
                        VerticalSlash();
                    }
                    if (Input.GetKeyDown(KeyCode.RightControl))
                    {
                        HorizontalSlash();
                    }
                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        Stab();
                    }
                }
            }
        }
        #endregion

    }

    // bool attacking = true when attack start --> turn false when animation ends
    #region Attacks
    private void Stab()
    {
        Debug.Log("stab");
        attacking = true;  //       TODO: USE public override OnStateExit, -Enter to change attacking bool
        StartCoroutine(Timer());
        // test purposes only
    }

    private void HorizontalSlash()
    {
        Debug.Log("horizontalslash");
        attacking = true;
        StartCoroutine(Timer());
    }

    private void VerticalSlash()
    {
        Debug.Log("verticalslash");
        attacking = true;
        StartCoroutine(Timer());
    }

    private void ChangeGuard()
    {
        Debug.Log("changeguard");
        attacking = true;
        StartCoroutine(Timer());
    }

    private void ChangeSide()
    {
        Debug.Log("changeside");
        attacking = true;
        StartCoroutine(Timer());
    }

    float timer = 1;

    private void TurnSword(int index)
    {
        attacking = true;

        swordAngle += index;

        if (swordAngle < 0)
            swordAngle = 3;
        else if (swordAngle > 3)
            swordAngle = 0;

        StartCoroutine(Timer());

        Debug.Log(index + " " + swordAngle);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        attacking = false;
    }
    #endregion

    public void SetInputAxis(string hori, string vert)
    {
        horizontal = hori;
        vertical = vert;
    }

    public void PlaySound(string clipName)
    {
        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlaySound(clipName);
        }
    }

}
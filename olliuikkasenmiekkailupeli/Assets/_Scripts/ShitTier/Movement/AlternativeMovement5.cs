using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeMovement5 : MonoBehaviour
{
    #region Scripts
    PlayerDistance playerDistance;
    InputManager im;
    HandAnimationControl hand;
    #endregion

    #region PlayerInfos
    public int playerIndex;

    [Header("----- Player Movement Axis Names -----")]
    [SerializeField] string horizontal;
    [SerializeField] string vertical;

    [SerializeField]
    float inputX, inputY, speed = 3f;

    float xLimit = 5f;
    float mouseX;

    [Header("--- Inputs ---")]
    [SerializeField] bool forward;
    [SerializeField] bool back;
    [SerializeField] bool attacking;

    [SerializeField]
    Transform p1StartPos, p2StartPos;

    [SerializeField]
    bool facingRight = false;

    [SerializeField]
    int controllerLayout;

    [SerializeField]
    float hor, ver;

    bool canBackup;

    Animator[] anims;
    Animator anim;
    #endregion


    private void Awake()
    {
        SetPositionAndRotationToPlayers();
    }

    void Start()
    {
        controllerLayout = 1;
        playerDistance = FindObjectOfType<PlayerDistance>();
        im = FindObjectOfType<InputManager>();
        hand = GetComponentInChildren<HandAnimationControl>();
        FindActiveComponents();
    }

    void FindActiveComponents()
    {
        anims = GetComponentsInChildren<Animator>();

        for (int i = 0; i < anims.Length; i++)
        {
            if (anims[i].enabled)
                anim = anims[i];
        }
    }

    void SetPositionAndRotationToPlayers()
    {
        p1StartPos = GameObject.Find("P1_StartPosition").gameObject.transform;
        p2StartPos = GameObject.Find("P2_StartPosition").gameObject.transform;

        #region RotatePlayersRight

        if(InputManager.IM.isLeftP1 && playerIndex == 1)
        {
            facingRight = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (InputManager.IM.isLeftP2 && playerIndex == 2)
        {
            facingRight = true;
            transform.localScale = new Vector3(-1, 1, 1);
            transform.Rotate(0, 180, 0);
        }
        else if(InputManager.IM.isRightP1 && playerIndex == 1)
        {
            facingRight = false;
            transform.localScale = new Vector3(1, 1, 1);
            transform.Rotate(0, 180, 0);
        }
        else if(InputManager.IM.isRightP2 && playerIndex == 2)
        {
            facingRight = false;
            transform.localScale = new Vector3(1, 1, 1);
        }

        if(facingRight)
        {
            transform.position = p1StartPos.position;
        }
        else
        {
            transform.position = p2StartPos.position;
        }

        #endregion
    }

    // ----- AFTER START -----

    public bool GetFacingRight(int PlayerNumber)
    {
        return facingRight;
    }

    void Update()
    {
        Inputs();
        Move();
        canBackup = playerDistance.CanBackup();
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

        #region inputBools
        if(facingRight)
        {

            if (Input.GetAxis(horizontal) >= .1f)
            {
                forward = true;
            }
            else
            {
                forward = false;
            }

            if (Input.GetAxis(horizontal) <= -.1f && canBackup)
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
            if (Input.GetAxis(horizontal) >= .1f && canBackup)
            {
                back = true;
            }
            else
            {
                back = false;
            }

            if (Input.GetAxis(horizontal) <= -.1f)
            {
                forward = true;
            }
            else
            {
                forward = false;
            }
        }
        #endregion

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

    }

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
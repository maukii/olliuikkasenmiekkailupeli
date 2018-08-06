using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeMovement5 : MonoBehaviour
{
    #region Scripts

    Distances distances;
    InputManager im;
    HandAnimationControl hand;

    #endregion

    #region PlayerInfos
    public int playerIndex;
    public float playerMinDistance = 1f;

    [Header("--- Player Axises ---")]
    [SerializeField] string horizontal;
    [SerializeField] string vertical;

    float inputX, inputY, speed = 3f;

    float xLimit = 5f;
    float mouseX;

    [Header("--- Inputs ---")]
    [SerializeField] bool forward;
    [SerializeField] bool back;
    [SerializeField] bool attacking;

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

    HandAnimationControl[] scripts;
    HandAnimationControl script;

    GameObject P1, P2;
    #endregion


    float playerDistance;

    private void Awake()
    {
        SetPositionAndRotationToPlayers();
    }

    void Start()
    {
        P1 = GameObject.Find("P1").gameObject;
        P2 = GameObject.Find("P2").gameObject;

        p1StartPos = GameObject.Find("P1_StartPosition").gameObject.transform;
        p2StartPos = GameObject.Find("P2_StartPosition").gameObject.transform;

        controllerLayout = 1;
        distances = FindObjectOfType<Distances>();
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

        scripts = GetComponentsInChildren<HandAnimationControl>();

        for (int i = 0; i < scripts.Length; i++)
        {
            if (scripts[i].enabled)
                script = scripts[i];
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

    public bool GetFacingRight(int PlayerNumber)
    {
        return facingRight;
    }

    // ----- AFTER START -----

    void Update()
    {
        Inputs();
        Move();

        canBackup = distances.CanBackUp(playerIndex);
        playerDistance = distances.GetPlayerDistance();

        if(PauseMenu.gameIsPaused)
        {
            script.enabled = false;
        }
        else
        {
            script.enabled = true;
        }
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

            if (Input.GetAxis(horizontal) >= .1f && playerDistance > playerMinDistance)
            {
                forward = true;
            }
            else if(Input.GetAxis(horizontal) >= .1f && playerDistance <= playerMinDistance)
            {
                forward = false;

                if(playerIndex == 1)
                {
                    // P2 jumps back
                }
                else
                {
                    // P1 jumps back
                }
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

            if (Input.GetAxis(horizontal) <= -.1f && playerDistance > playerMinDistance)
            {
                forward = true;
            }
            else if (Input.GetAxis(horizontal) >= .1f && playerDistance <= playerMinDistance)
            {
                forward = false;

                if (playerIndex == 1)
                {
                    // P2 jumps back
                }
                else
                {
                    // P1 jumps back
                }
            }
            else
            {
                forward = false;
            }
        }
        #endregion

        #region AnimatonStuffs

        anim.SetFloat("InputX", hor);
        anim.SetBool("forward", forward);
        anim.SetBool("back", back);

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
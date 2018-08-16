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
    [SerializeField]
    string horizontal;
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

    [SerializeField] bool canBackup;
    [SerializeField] bool canMoveForward;

    Animator[] anims;
    Animator anim;

    HandAnimationControl[] scripts;
    HandAnimationControl script;

    GameObject P1, P2;
    #endregion

    [SerializeField] Animator otherPlayerAnim;
    float playerDistance;

    private void Awake()
    {
        SetPositionAndRotationToPlayers();
    }

    void Start()
    {
        P1 = GameObject.FindGameObjectWithTag("Player 1");
        P2 = GameObject.FindGameObjectWithTag("Player 2");

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

        if (InputManager.IM.isLeftP1 && playerIndex == 1)
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
        else if (InputManager.IM.isRightP1 && playerIndex == 1)
        {
            facingRight = false;
            transform.localScale = new Vector3(1, 1, 1);
            transform.Rotate(0, 180, 0);
        }
        else if (InputManager.IM.isRightP2 && playerIndex == 2)
        {
            facingRight = false;
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (facingRight)
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

    public Animator GetActiveAnimator()
    {
        return anim;
    }

    public HandAnimationControl GetActiveHandScript()
    {
        return script;
    }

    // ----- AFTER START -----

    bool gotAnim;

    void Update()
    {
        if (GameHandler.instance.BattleStarted && !gotAnim)
        {
            if (playerIndex == 1)
                otherPlayerAnim = P2.GetComponent<AlternativeMovement5>().GetActiveAnimator();
            else if (playerIndex == 2)
                otherPlayerAnim = P1.GetComponent<AlternativeMovement5>().GetActiveAnimator();
            if (otherPlayerAnim != null)
                gotAnim = true;
        }

        if(!GameHandler.instance.battleEnded)
        {
            Inputs();
            Move();

            canBackup = distances.CanBackUp(playerIndex);
            canMoveForward = distances.CanMoveForwards();
            playerDistance = distances.GetPlayerDistance();

            if (PauseMenu.gameIsPaused)
            {
                script.enabled = false;
                anim.enabled = false;
            }
            else
            {
                script.enabled = true;
                anim.enabled = true;
            }
        }
        else
        {
            anim.SetBool("back", false);
            anim.SetBool("forward", false);
            anim.SetBool("Strong", false);
            anim.SetBool("SwingDia", false);
            anim.SetBool("SwingHor", false);
            anim.SetBool("Deflect", false);
            anim.SetBool("Interrupt", false);
            anim.SetBool("light", false);
            anim.SetBool("ADeflect", false);
            anim.SetBool("AExtend", false);
            anim.SetBool("ALight", false);
            anim.SetBool("Idle", true);
            anim.SetBool("Lunged", false);
            anim.SetBool("Jumped", false);
        }
    }

    void Inputs()
    {
        mouseX = Input.GetAxis("MouseX");
        hor = Input.GetAxis(horizontal);

        inputX = Mathf.Clamp(inputX, -1, 1);
        inputY = Mathf.Clamp(inputY, -1, 1);

        if (playerIndex == 1)
        {
            if (im.isXboxControllerP1)
                ver = -Input.GetAxis(vertical);
            else
            {
                //ver = Input.GetAxis(vertical);
                ver = Mathf.Clamp(Input.GetAxis(vertical), -1, 1);
            }

        }
        else if (playerIndex == 2)
        {
            if (im.isXboxControllerP2)
                ver = -Input.GetAxis(vertical);
            else
                //ver = Input.GetAxis(vertical);
                ver = Mathf.Clamp(Input.GetAxis(vertical), -1, 1);
        }

    }

    // lunge variables
    public float timer = .5f;
    float defaultTimer = .5f;

    [SerializeField] bool holdingForward, firstF, lunged;
    [SerializeField] bool holdingBack, firstB, jumped;

    [SerializeField] float extraDistance = 0.1f;

    void Move()
    {

        #region ControllerLayout
        if (playerIndex == 1)
        {
            if (im.P1_Dpad_Y == 1)
            {
                controllerLayout = 1;
            }
            else if (im.P1_Dpad_X == -1)
            {
                controllerLayout = 2;
            }
            else if (im.P1_Dpad_Y == -1)
            {
                controllerLayout = 3;
            }
            else if (im.P1_Dpad_X == 1)
            {
                controllerLayout = 4;
            }
        }
        if (playerIndex == 2)
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
        if (facingRight)
        {

            if (Input.GetAxis(horizontal) >= .1f && playerDistance > playerMinDistance)
            {
                forward = true;
            }
            else if (Input.GetAxis(horizontal) >= .1f && playerDistance <= playerMinDistance)
            {
                forward = false;
                anim.SetBool("TryToMoveForward", true);

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
                anim.SetBool("TryToMoveForward", false);
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
                anim.SetBool("TryToMoveForward", true);

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
                anim.SetBool("TryToMoveForward", false);
                forward = false;
            }
        }
        #endregion


        #region Lunge

        if (firstF)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            timer = defaultTimer;
            firstF = false;
        }

        if (Input.GetAxisRaw(horizontal) == 1 * (facingRight ? 1 : -1))
        {
            holdingForward = true;
        }

        if (holdingForward && Input.GetAxisRaw(horizontal) != 1 * (facingRight ? 1 : -1))
        {
            holdingForward = false;
            firstF = true;
        }

        if (firstF && Input.GetAxisRaw(horizontal) == 1 * (facingRight ? 1 : -1) && playerDistance > playerMinDistance)
        {
            if (!lunged && !jumped)
            {
                if((otherPlayerAnim.GetBool("Lunged") && playerDistance > playerMinDistance + extraDistance) || !otherPlayerAnim.GetBool("Lunged"))
                {
                    //anim.CrossFade("Lunge2", .5f);
                    anim.SetBool("Lunged", true);
                    firstF = false;
                    holdingForward = false;
                }
            }
        }     

        #endregion


        #region JumpBackwards

        if (firstB)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            timer = defaultTimer;
            firstB = false;
        }

        if (Input.GetAxisRaw(horizontal) == -1 * (facingRight ? 1 : -1))
        {
            holdingBack = true;
        }

        if (holdingBack && Input.GetAxisRaw(horizontal) != -1 * (facingRight ? 1 : -1))
        {
            holdingBack = false;
            firstB = true;
        }

        if (firstB && Input.GetAxisRaw(horizontal) == -1 * (facingRight ? 1 : -1))
        {
            if(canBackup && !jumped)
            {
                if(!lunged)
                {
                    //anim.CrossFade("Jump", .5f);
                    anim.SetBool("Jumped", true);
                    firstB = false;
                    holdingBack = false;
                }
                else
                {
                    //anim.CrossFade("Jump_Lunge", .5f);
                    anim.SetBool("Jumped", true);
                    firstB = false;
                    holdingBack = false;
                }
            }
        }

        #endregion


        #region AnimatonStuffs

        jumped = anim.GetBool("Jumped");
        lunged = anim.GetBool("Lunged");
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
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySound(clipName);
        }
    }
}
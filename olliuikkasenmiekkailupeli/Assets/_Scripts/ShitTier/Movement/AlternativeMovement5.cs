using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeMovement5 : MonoBehaviour
{
    public int playerIndex;

    public Transform p1StartPos, p2StartPos;
    Quaternion p1StartRot, p2StartRot;

    public bool trollModel;
    public bool facingRight = false;
    public bool usingXbox;

    public float hor, ver;
    Animator[] anims;
    Animator anim;

    [Header("----- Player Movement Axis Names -----")]
    public string horizontal;
    public string vertical;

    public float inputX, inputY;
    public float speed = 3f;

    [Header("--- Inputs ---")]
    public bool forward;
    public bool back;
    // up, down

    void Start()
    {
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
        p1StartRot = GameObject.Find("P1").gameObject.transform.rotation;
        p2StartRot = GameObject.Find("P2").gameObject.transform.rotation;

        if (InputManager.IM.isLeftP1 && playerIndex == 1)
        {
            facingRight = true;
        }
        else if (InputManager.IM.isLeftP2 && playerIndex == 2)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
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
    } // check which player was left side

    void Update()
    {
        if(usingXbox)
        {
            ver = -Input.GetAxis(vertical);
        }
        else
        {
            ver = Input.GetAxis(vertical);
        }

        hor = Input.GetAxis(horizontal);
        
        inputX = Mathf.Clamp(inputX, -1, 1);
        inputY = Mathf.Clamp(inputY, -1, 1);

        Move();
    } // animator stuff

    void Move()
    {

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
    }

    public void SetInputAxis(bool xBox, string hori, string vert)
    {
        usingXbox = xBox;
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

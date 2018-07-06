using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeMovement4 : MonoBehaviour
{
    public bool facingRight = false;

    public float hor, ver;
    Animator anim;

    [Header("----- Player Movement Axis Names -----")]
    public string horizontal = "Horizontal";
    public string vertical = "Vertical";

    public float inputX, inputY;
    public float speed = 3f;

    [Header("--- Inputs ---")]
    public int controllerNumber;
    public bool forward;
    public bool back;
    // up, down

    [ExecuteInEditMode]
    private void Awake()
    {
        if (facingRight)
        {
            transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y - 1f, transform.parent.transform.position.z);
            transform.parent.Rotate(-90f, 0f, 0f);
        }
        else
        {
            transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y - 1f, transform.parent.transform.position.z);
            transform.parent.transform.Rotate(-90f, 0, 0);
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        hor = Input.GetAxis(horizontal);
        ver = Input.GetAxis(vertical);

        inputX = Mathf.Clamp(inputX, -1, 1);
        inputY = Mathf.Clamp(inputY, -1, 1);

        Move();

        // TODO: use .CrossFaid to take damage animation clip
        if (Input.GetKeyDown(KeyCode.Space))
            AudioManager.instance.PlaySound("swordcollide");
    }

    void Move()
    {

        #region inputBools
        if(Input.GetAxisRaw(horizontal) == -1)
        {
            forward = true;
        }
        else
        {
            forward = false;
        }

        if(Input.GetAxisRaw(horizontal) == 1)
        {
            back = true;
        }
        else
        {
            back = false;
        }
        #endregion

        anim.SetFloat("InputX", -hor);
        anim.SetBool("forward", forward);
        anim.SetBool("back", back);

        #region inputX
        //if (hor >= 0.5f && inputX < 1f)
        //{
        //    inputX += speed * Time.deltaTime;
        //    anim.SetFloat("InputX", inputX);
        //}
        //if (hor <= -0.5f && inputX > -1f)
        //{
        //    inputX -= speed * Time.deltaTime;
        //    anim.SetFloat("InputX", inputX);
        //}
        //
        //if (hor == 0)
        //{
        //    inputX = Mathf.Lerp(inputX, 0, speed * Time.deltaTime);
        //    if ((inputX <= 0.3f && inputX > 0) || (inputX >= -0.3f && inputX < 0))
        //    {
        //        inputX = 0f;
        //    }
        //
        //    anim.SetFloat("InputX", inputX);
        //
        //}
        #endregion

        if (ver >= 0.1f && inputY < 1f)
        {
            inputY += speed * Time.deltaTime;
            anim.SetFloat("InputY", inputY);
        }
        if (ver <= -0.1f && inputY > -1f)
        {
            inputY -= speed * Time.deltaTime;
            anim.SetFloat("InputY", inputY);
        }

        if (ver == 0)
        {
            inputY = Mathf.Lerp(inputY, 0, speed * Time.deltaTime);
            if ((inputY <= 0.02f && inputY > 0) || (inputY >= -0.02f && inputY < 0))
            {
                inputY = 0f;
            }
            anim.SetFloat("InputY", inputY);
        }
    }

    public void PlaySound(string clipName)
    {
       AudioManager.instance.PlaySound(clipName);
    }

}

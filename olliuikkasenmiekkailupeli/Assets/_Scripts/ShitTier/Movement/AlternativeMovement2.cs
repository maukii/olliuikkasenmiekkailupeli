using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeMovement2 : MonoBehaviour
{

    public float hor, ver;
    float handMovementW = 1f, handAttackW = 1f;
    int handMovement = 1, handAttack = 2;
    Animator anim;

    [Header("----- Player Movement Axis Names -----")]
    public string horizontal;
    public string vertical;
    public KeyCode action;
    public float attackTimer;

    public float inputX, inputY;
    public float speed = 3f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        hor = Input.GetAxis(horizontal);
        ver = Input.GetAxisRaw(vertical);

        inputX = Mathf.Clamp(inputX, -1, 1);
        inputY = Mathf.Clamp(inputY, -1, 1);

        Move();
        GetLayerWeights();
    }

    void Move()
    {

        if (hor >= 0.5f && inputX < 1f)
        {
            inputX += speed * Time.deltaTime;
            anim.SetFloat("InputX", inputX);
        }
        if( hor <= -0.5f && inputX > -1f)
        {
            inputX -= speed * Time.deltaTime;
            anim.SetFloat("InputX", inputX);
        }
        

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

        if(hor == 0)
        {
            inputX = Mathf.Lerp(inputX, 0, speed * Time.deltaTime);
            if ((inputX <= 0.02f && inputX > 0) || (inputX >= -0.02f && inputX < 0))
            {
                inputX = 0f;
            }

            anim.SetFloat("InputX", inputX);

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

        // this means attack button is pressed
        if(Input.GetKey(action))
        {
            anim.SetBool("ActionHold", true);
            anim.SetFloat("ActionTimer", attackTimer);

            attackTimer += Time.deltaTime;
            if (attackTimer >= 1f)
            {
                attackTimer = 1f;
            } 

            anim.SetLayerWeight(handMovement, handMovementW -= Time.deltaTime);
            anim.SetLayerWeight(handAttack, /* handAttackW += Time.deltaTime */ 0.75f + Time.deltaTime); // TODO: tweak these numbers
        }
        else // not attacking
        {
            anim.SetBool("ActionHold", false);
            anim.SetFloat("ActionTimer", attackTimer);

            attackTimer -= Time.deltaTime;
            if(attackTimer <= 0f)
            {
                attackTimer = 0f;
            }

            anim.SetLayerWeight(handMovement, handMovementW += Time.deltaTime);
            anim.SetLayerWeight(handAttack, handAttackW -= Time.deltaTime);
        }

    }

    // how to change weight between anim layers
    void GetLayerWeights()
    {
        handMovementW = anim.GetLayerWeight(1);
        handAttackW = anim.GetLayerWeight(2);

        if (handMovementW >= 1f)
            handMovementW = 1f;
        if (handMovementW <= 0f)
            handMovementW = 0f;
        if (handAttackW >= 1f)
            handAttackW = 1f;
        if (handAttackW <= 0f)
            handAttackW = 0f;
    }

    // used for animation event
    public void ResetAttackTime()
    {
        attackTimer = 0f;
    }

}

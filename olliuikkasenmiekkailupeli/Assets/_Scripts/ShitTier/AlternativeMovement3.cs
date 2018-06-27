using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeMovement3 : MonoBehaviour
{

    public float hor, ver;
    Animator anim;

    [Header("----- Player Movement Axis Names -----")]
    public string horizontal;
    public string vertical;
    public KeyCode action;
    public float attackTimer;

    public float inputX, inputY;
    public float speed = 3f;

    int handControllLayer = 1, handAttackLayer = 2;
    float handMovementW, handAttackW;

    Vector3 target;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        target = transform.position;

        hor = Input.GetAxis(horizontal);
        ver = Input.GetAxisRaw(vertical);

        inputX = Mathf.Clamp(inputX, -1, 1);
        inputY = Mathf.Clamp(inputY, -1, 1);

        GetLayerWeights();
        Action();
        Move();      
    }

    void Move()
    {

        if (hor >= 0.5f && inputX < 1f)
        {
            inputX += speed * Time.deltaTime;
            anim.SetFloat("InputX", inputX);
        }
        if (hor <= -0.5f && inputX > -1f)
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

        if (hor == 0)
        {
            inputX = Mathf.Lerp(inputX, 0, speed * Time.deltaTime);
            if ((inputX <= 0.3f && inputX > 0) || (inputX >= -0.3f && inputX < 0))
            {
                inputX = 0f;
            }

            anim.SetFloat("InputX", inputX);

        }

        if (ver == 0)
        {
            inputY = Mathf.Lerp(inputY, 0, speed * Time.deltaTime);
            if ((inputY <= 0.3f && inputY > 0) || (inputY >= -0.3f && inputY < 0))
            {
                inputY = 0f;
            }

            anim.SetFloat("InputY", inputY);

        }

    }

    void Action()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            anim.SetTrigger("Step");
            target = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            anim.SetTrigger("Step2");
        }

        if(Input.GetKey(action))
        {
            //anim.SetBool("Holding", true);

            if(attackTimer < 1f)
            {
                attackTimer += Time.deltaTime;
            }

            anim.SetLayerWeight(handAttackLayer, 1);
            //anim.SetLayerWeight(handControllLayer, handMovementW -= Time.deltaTime);

        }
        else
        {
            //anim.SetBool("Holding", false);

            if(attackTimer > 0f)
            {
                attackTimer -= Time.deltaTime;
            }

            //anim.SetLayerWeight(handControllLayer, handMovementW += Time.deltaTime);
            anim.SetLayerWeight(handAttackLayer, 0);

        }

        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        anim.SetFloat("AttackTimer", attackTimer);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAnimationTest : MonoBehaviour {
    Animator anim;
    public Animator otherAnim;
    float inside;
    //float hanging;
    float otherInside;
    float otherHanging;
    AnimatorStateInfo asi;
    //HandAnimationControl hac;
    float interruptTimer;
    int swingHash;
    public float CollisionTimeWeak = 0.3f;
    public float CollisionTimeStrong = 0.7f;
    
    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
        //hac = gameObject.GetComponent<HandAnimationControl>();
        asi = anim.GetCurrentAnimatorStateInfo(0);
        interruptTimer = 0;
        swingHash = 0;
    }
	
	// Update is called once per frame
	void Update () {
        inside = anim.GetFloat("Inside");
        //hanging = anim.GetFloat("Hanging");
        otherInside = otherAnim.GetFloat("Inside");
        otherHanging = otherAnim.GetFloat("Hanging");
        asi = anim.GetCurrentAnimatorStateInfo(0);
        if (asi.IsTag("Swing"))
        {
            Debug.Log("mo");
            swingHash = asi.fullPathHash;
            float collidetime = anim.GetFloat("Strong") == 1 ? CollisionTimeStrong : CollisionTimeWeak;

            if (asi.normalizedTime > collidetime)
            {
                if (CheckHeight())
                {
                    if (CheckQuard())
                    {
                        Deflect();
                    }
                    else
                    {
                        QuardBreak();
                    }
                }
            }
        }
        if(asi.fullPathHash != swingHash)
        {
            anim.SetFloat("SpeedMult", 1);
            swingHash = asi.fullPathHash;
        }


        if (interruptTimer > 0)
        {
            interruptTimer -= Time.deltaTime;
        }
        else
        {
            interruptTimer = 0;
            otherAnim.SetBool("Deflect", false);
            otherAnim.SetBool("Interrupt", false);
        }
    }
    void Deflect()
    {
        interruptTimer = 1;
        otherAnim.SetBool("Deflect", true);
    }
    bool CheckQuard()
    {
        float direction;
        direction = inside * 2 - 1;
        float guard;
        guard = otherHanging == otherInside ? 1 : -1;
        if(direction == guard)
        {
            return true;
        }
        return false;
    }
    bool CheckHeight()
    {
        return true;
    }
    void QuardBreak()
    {
        otherAnim.SetBool("Interrupt", true);
        interruptTimer = 1;
    }
}

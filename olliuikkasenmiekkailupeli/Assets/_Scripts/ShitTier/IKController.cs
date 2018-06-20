using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKController : MonoBehaviour
{
    Animator anim;

    public bool ikActive = false;

    public Transform leftHandObj = null;
    public Transform lookObj = null;

    void Start ()
    {
        anim = GetComponent<Animator>();
	}

    public int layer;

    private void Update()
    {
        OnAnimatorIK(layer);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Step");
        }
    }

    void OnAnimatorIK(int layerIndex)
    {
        if(anim)
        {
            if(ikActive)
            {
                if (lookObj != null)
                {
                    anim.SetLookAtWeight(1);
                    anim.SetLookAtPosition(lookObj.position);
                }

                if (leftHandObj != null)
                {
                    anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                    anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
                }
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                anim.SetLookAtWeight(0);
            }

        }
    }

}

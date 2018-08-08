using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationIdleOverride : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Idle", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Idle", false);
    }

}

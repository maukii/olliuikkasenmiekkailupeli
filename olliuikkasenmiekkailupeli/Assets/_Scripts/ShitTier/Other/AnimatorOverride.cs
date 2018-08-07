using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverride : StateMachineBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetLayerWeight(1, 0); // when animation starts set hands layer weight to 0
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex); // when animation ends set hands layer weight to 1
        animator.SetLayerWeight(1, 1);
    }
}

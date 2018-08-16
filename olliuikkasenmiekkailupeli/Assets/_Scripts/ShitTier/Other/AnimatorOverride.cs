using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverride : StateMachineBehaviour {

    bool ready;
    public int startAnimation;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        startAnimation = Random.Range(0, 2);
        animator.SetLayerWeight(1, 0);
        animator.SetInteger("StartAnim", startAnimation);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.SetLayerWeight(1, 1);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1.0f, Time.deltaTime * 1f));
    } 

}

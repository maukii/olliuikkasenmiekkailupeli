using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnimationOverride : StateMachineBehaviour
{

    [SerializeField] float value;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetLayerWeight(1, value);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.SetLayerWeight(1, 1);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        animator.SetLayerWeight(1, Mathf.Lerp(value, 1, Time.deltaTime * 5));
    }

}
